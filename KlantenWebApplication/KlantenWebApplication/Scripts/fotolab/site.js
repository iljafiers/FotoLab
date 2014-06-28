var Info = {
	baseApiUrl: "http://localhost:2372/"
};

// usage: self.flashmessage.addMessage("Dit is een message");
function FlashMessages() {
	var self = this;

	self.message = ko.observable("");
	self.messages = [];

	self.addMessage = function(message, bootstrapColor, time) {
		if(typeof bootstrapColor === "undefined") { bootstrapColor = "danger"; }
		if(typeof time === "undefined" || time === 0 ) { time = 4000; }
		self.messages.push( { message: message, bootstrapColor: bootstrapColor, time: time } );
		self.nextMessage();
	};

	self.nextMessage = function() {
		if( self.message().length === 0) {
			if( self.messages.length > 0) {
				var message = self.messages.shift();

				$("#flashMessage").removeAttr("class").addClass("alert alert-" + message.bootstrapColor);

				self.message(message.message);

				window.setTimeout(function(){
					self.message("");
					self.nextMessage();
				}, message.time);
			}
		}
	};
}

function Validation() {
	
}


function Section(template, titel, enableNext) {
	var self = this;

	self.template = template;
	self.titel = ko.observable(titel);
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

	/*self.getFotos = function() {
		$.getJSON("http://localhost:2372/api/fotoserie/", function(dataArr) {
			var fss = $.map(dataArr, function(datarow) {
				return new Fotoserie(datarow.serie_key, datarow.naam, datarow.fotos);
			});
			self.fotoseries(fss);
		});
	};*/
}

function Klant() {
	var self = this;
	self.key = ko.observable("");
	self.fotoseries = ko.observableArray();

	self.naam = ko.observable("");
	self.straat = ko.observable("");
	self.huisnummer = ko.observable("");
	self.postcode = ko.observable("");
	self.woonplaats = ko.observable("");

	var getFotoseries = function(key) {
		console.log("getFotoseries");
		if( key.length > 1 ) {
			//$.getJSON("http://localhost:2372/api/klant/" + self.key() + "/fotoseries");
			$.getJSON(Info.baseApiUrl + "api/klant/" + key + "/fotoserie/", function(dataArr) {
				var fss = $.map(dataArr, function(datarow) {
					return new Fotoserie(datarow.serie_key, datarow.naam, datarow.fotos);
				});
				self.fotoseries(fss);
			});
		}
	};

	var getKlantDetails = function(key) {
		if( key.length > 1 ) {
			$.getJSON(Info.baseApiUrl + "api/klant/" + key, function(data) {
				console.dir(data);
				self.naam(data.Naam);
				self.straat(data.Straat);
				self.huisnummer(data.Huisnummer);
				self.postcode(data.Postcode);
				self.woonplaats(data.Woonplaats);				
			});
		}
	};

	self.reset = function() {
		self.key = ko.observable("");
		self.fotoseries = ko.observableArray().removeAll();

		self.naam = ko.observable("");
		self.straat = ko.observable("");
		self.huisnummer = ko.observable("");
		self.postcode = ko.observable("");
		self.woonplaats = ko.observable("");
	};

	self.key.subscribe(getKlantDetails);
	self.key.subscribe(getFotoseries);
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

	self.reset = function() {
		self.items.removeAll();
	};
}

function fotolabViewModel() {
	var self = this;

	self.flashmessage = new FlashMessages();

	self.klant = new Klant();
	self.order = new Order(self.klant);
	self.fotoserie = ko.observable();

	self.sections = ko.observableArray([
		new Section("fotolab_login", "Login", function() {  return ( (self.klant.key().length > 0) && (self.klant.naam().length > 0) ); } ),
		new Section("fotolab_klantgegevens", "Klantgegevens", function() { return true; } ),
		new Section("fotolab_select_fotoserie", "Selecteer fotoserie", function() { return true; } ),
		new Section("fotolab_select_fotos", "Selecteer foto's", function() { return true; } ),
		new Section("fotolab_productlist", "Productenlijst", function() { return true; } ),
		new Section("fotolab_payment", "Betaalmethode", function() { return true; } ),
		new Section("fotolab_thanks", "Bedankt voor uw bestelling", function() { return true; } )
	]);
	self.currentSectionKey = ko.observable(0);

	self.currentSection = ko.computed(function() {
		return self.sections()[self.currentSectionKey()];
	});

	self.nextEnabled = ko.computed(function() {	
		return ( self.currentSectionKey() < (self.sections().length - 1) ) && 
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
		self.klant.reset();
		self.order.reset();
		self.fotoserie(null);
	};

	self.displaySection = function() {
		return self.currentSection().template;
	};
	self.enableNext = function() {
		self.sections()[self.currentSectionKey() + 1].enabled(true);
	};

	self.showDebug = function() {
		self.flashmessage.addMessage("Hallo", "danger", 300);
		self.flashmessage.addMessage("dit", "info", 300);
		self.flashmessage.addMessage("is", "success", 300);
		self.flashmessage.addMessage("een", "warning", 300);
		self.flashmessage.addMessage("message!", "default", 400);
	};

	self.headingTitle = ko.computed(function() {
		return "Stap " + (self.currentSectionKey() + 1) + ": " + self.currentSection().titel();
	});
}


$(document).ready(function() {
	ko.applyBindings(new fotolabViewModel());
});
