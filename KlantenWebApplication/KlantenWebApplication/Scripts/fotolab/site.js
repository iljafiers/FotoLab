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
	self.
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


}

ko.applyBindings(new fotolabViewModel());