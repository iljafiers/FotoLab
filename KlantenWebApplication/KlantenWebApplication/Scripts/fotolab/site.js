function Section(template, titel, enabled, enableNext) {
	var self = this;

	self.template = template;
	self.titel = ko.observable(titel);
	self.enabled = ko.observable(enabled);
	self.enableNext = enableNext; // function
}

function Extra(naam, bedrag) {
	var self = this;
	self.naam = naam;
	self.bedrag = bedrag;
}

function Item(foto) {
	var self = this;

	self.foto = ko.observable(foto);
	self.extra = ko.observable();

	self.subtotal = ko.computed(function() {
		return self.foto().bedrag + self.extra().bedrag;
	});	
}

/*function Foto(id, fotoserie) {
	var self = this;

	self.id = id;
	self.fotoserie = fotoserie;

}*/

function Fotoserie(key, naam, fotoIds) {
	var self = this;

	self.key = key;
	self.naam = naam;
	self.fotoIds = fotoIds;

	self.getFotos = function() {
		$.getJSON("http://localhost:2372/api/fotoserie/", function(dataArr) {
			var fss = $.map(dataArr, function(datarow) {
				return new Fotoserie(datarow.serie_key, datarow.naam, datarow.fotos);
			});
			self.fotoseries(fss);
		});
	};
}

function Klant() {
	var self = this;
	self.key = ko.observable();
	self.fotoseries = ko.observableArray();

	self.naam = ko.observable();
	self.straat = ko.observable();
	self.huisnummer = ko.observable();
	self.postcode = ko.observable();
	self.woonplaats = ko.observable();

	self.getFotoseries = ko.computed(function() {
		//$.getJSON("http://localhost:2372/api/klant/" + self.key() + "/fotoseries");
		$.getJSON("http://localhost:2372/api/fotoserie/", function(dataArr) {
			var fss = $.map(dataArr, function(datarow) {
				return new Fotoserie(datarow.serie_key, datarow.naam, datarow.fotos);
			});
			self.fotoseries(fss);
		});
	});
}

function Order(klant) {
	var self = this;

	self.klant = klant;
	self.items = ko.observableArray();

	self.total = ko.computed(function() {
		var total = 0;
		$.each(self.items(), function() { total += this.subtotal(); });
		return total;
	});
}

function fotolabViewModel() {
	var self = this;

	self.klant = new Klant();
	self.order = new Order();
	self.fotoserie = ko.observable();

	self.sections = ko.observableArray([
		new Section("fotolab_login", "Login", true, function() {  return false; } ),
		new Section("fotolab_select_fotoserie", "Selecteer fotoserie", false, function() { return true; } ),
		new Section("fotolab_select_fotos", "Selecteer foto's", false, function() { return true; } ),
		new Section("fotolab_productlist", "Productenlijst", false, function() { return true; } ),
		new Section("fotolab_klantgegevens", "Klantgegevens", false, function() { return true; } ),
		new Section("fotolab_payment", "Betaalmethode", false, function() { return true; } ),
		new Section("fotolab_thanks", "Bedankt voor uw bestelling", false, function() { return true; } )
	]);
	self.currentSectionKey = ko.observable(0);

	/*self.getSections = function() {
		self.sections.removeAll();
		
		$("#fotolabsite .section").each(function() {
			self.sections.push( new Section( $(this).attr("id") ) );
		});
	};*/

	self.currentSection = ko.computed(function() {
		return self.sections()[self.currentSectionKey()];
	});

	self.nextEnabled = ko.computed(function() {
		return ( self.currentSectionKey() < (self.sections().length - 1) ) && 
			  // ( self.sections()[self.currentSectionKey() + 1].enabled() );
			   ( self.currentSection().enableNext() );
	});
	self.previousEnabled = ko.computed(function() {
		return (self.currentSectionKey() > 0);
	});

	self.next = function() {
		if(self.nextEnabled()) { 
			self.currentSectionKey( self.currentSectionKey() + 1 );
		}
	};
	self.previous = function() {
		if(self.previousEnabled()) {
			self.currentSectionKey( self.currentSectionKey() - 1 );
		}
	};
	self.reset = function() {
		self.currentSectionKey(0);
	};

	self.displaySection = function() {
		return self.currentSection().template;
	};
	self.enableNext = function() {
		self.sections()[self.currentSectionKey() + 1].enabled(true);
	};
}


$(document).ready(function() {
	ko.applyBindings(new fotolabViewModel());
});
