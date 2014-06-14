function Section(domId) {
	var self = this;

	self.domId = ko.observable(domId);
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

function Foto(id, fotoserie) {
	var self = this;

	self.id = id;
	self.fotoserie = fotoserie;

}

function Fotoserie(key, fotos) {
	var self = this;

	self.key = key;
	self.fotos = fotos;
}

function Klant(naam) {
	var self = this;
	self.naam = ko.observable(naam);
	self.fotoseries = ko.observableArray();
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

	self.klant = ko.observable();
	self.order = ko.observable();

	self.sections = ko.observableArray();

	self.getSections = function() {
		$("#fotolabsite .section").each(function() {
			self.sections.push( new Section( $(this).attr("id") ) );
		});
	};
}

ko.applyBindings(new fotolabViewModel());