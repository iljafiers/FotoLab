ko.bindingHandlers.fadeVisible = {
    init: function(element, valueAccessor) {
        // Initially set the element to be instantly visible/hidden depending on the value
        var value = valueAccessor();
        $(element).toggle(ko.unwrap(value)); // Use "unwrapObservable" so we can handle values that may or may not be observable
    },
    update: function(element, valueAccessor) {
        // Whenever the value subsequently changes, slowly fade the element in or out
        var value = valueAccessor();
        ko.unwrap(value) ? $(element).fadeIn() : $(element).fadeOut();
    }
};

ko.bindingHandlers.fadeHidden = {
    init: function(element, valueAccessor) {
        // Initially set the element to be instantly visible/hidden depending on the value
        var value = valueAccessor();

        if( ko.unwrap(value) ) {
        	$(element).css('visibility','');
        } else {
        	$(element).css( 'visibility', 'hidden');
        }
    },
    update: function(element, valueAccessor) {
        // Whenever the value subsequently changes, slowly fade the element in or out
        var value = valueAccessor();
        if( ko.unwrap(value) ) {
        	$(element).css('visibility','visible');
        } else {
        	$(element).css('visibility','hidden');
        }
    }
};

ko.bindingHandlers.href = {
    update: function (element, valueAccessor) {
        ko.bindingHandlers.attr.update(element, function () {
            return { href: valueAccessor()}
        });
    }
};

ko.bindingHandlers.src = {
    update: function (element, valueAccessor) {
        ko.bindingHandlers.attr.update(element, function () {
            return { src: valueAccessor()}
        });
    }
};

var Utility = {
	random: function(min, max) {
		if(typeof min === "undefined") { min = 0; }
		if(typeof max === "undefined") { max = 10; }

		return Math.floor( Math.random() * ( max - min + 1 ) + min);
	}
};


var Info = {
	baseApiUrl: "http://localhost:2372/"
};

// usage: self.flashmessage.addMessage("Dit is een message");
function FlashMessages() {
	var self = this;

	self.message = ko.observable("m");
	self.messages = [];

	self.addMessage = function(message, bootstrapColor, time) {
		if(typeof bootstrapColor === "undefined") { bootstrapColor = "danger"; }
		if(typeof time === "undefined" || time === 0 ) { time = 5000; }
		self.messages.push( { message: message, bootstrapColor: bootstrapColor, time: time } );
		self.nextMessage();
	};

	self.nextMessage = function() {
		if( self.message().length === 1) {
			if( self.messages.length > 0) {
				var message = self.messages.shift();

				$("#flashMessage").removeAttr("class").addClass("alert alert-" + message.bootstrapColor);

				self.message(message.message);

				window.setTimeout(function(){
					self.message("m");
					self.nextMessage();
				}, message.time);
			}
		}
	};

	/*self.message.subscribe(function() { 
		console.log("Flashmessage length: " + self.message().length); 
		console.log("Flashmessage: " + self.message()); 
	});*/
}

function Section(template, titel, enableNext) {
	var self = this;

	self.template = template;
	self.titel = ko.observable(titel);
	self.enableNext = enableNext; // function to validate if next step should be enabled
	//self.onNext = onNext; 		  // function to fire on next
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

function Fotoserie(key, naam, datum, fotos) {
	var self = this;

	self.key = key;
	self.naam = naam;
	self.datum = datum;
	self.fotos = fotos;

	/*self.getFotos = function() {
		$.getJSON("http://localhost:2372/api/fotoserie/", function(dataArr) {
			var fss = $.map(dataArr, function(datarow) {
				return new Fotoserie(datarow.serie_key, datarow.naam, datarow.fotos);
			});
			self.fotoseries(fss);
		});
	};*/

	self.getFotoSrc = function(id) {
		console.log("getFotoSrc self.key(): " + self.key);
		console.log("getFotoSrc id: " + id);
		return Info.baseApiUrl + "api/fotoserie/" + self.key + "/foto/" + id;
	};

	self.randomFotoSrc = ko.computed(function() {
		if( self.fotos.length > 0) {
			var fotosKey = Utility.random(0, self.fotos.length - 1);
			var id = self.fotos[fotosKey].Id;
			return self.getFotoSrc(id);
		}
		return "";
	});
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

	var getFotoseries = function() {
		if( self.key().length > 1 ) {
			$.getJSON(Info.baseApiUrl + "api/klant/" + self.key() + "/fotoserie/", function(dataArr) {
				console.dir(dataArr);
				var fss = $.map(dataArr, function(datarow) {
					console.dir(datarow);
					return new Fotoserie(datarow.Key, datarow.Naam, datarow.Datum, datarow.Fotos);
				});
				self.fotoseries(fss);

				if( self.fotoseries().length === 0 ) {
					fotolab.vm.flashmessage.addMessage("U heeft geen fotoseries.", "danger");
				} else {
					fotolab.vm.flashmessage.addMessage("Fotoseries opgehaald!", "info");
				}
			});
		}
	};

	var getKlantDetails = function(key) {
		if( key.length > 1 ) {
			$.getJSON(Info.baseApiUrl + "api/klant/" + key, function(data) {
				if(data !== null) {
					fotolab.vm.flashmessage.addMessage("Klantgegevens opgehaald!", "success");
					if( data.Naam !== null ) { self.naam(data.Naam); };
					if( data.Straat !== null ) { self.straat(data.Straat); };
					if( data.Huisnummer !== null ) { self.huisnummer(data.Huisnummer); };
					if( data.Postcode !== null ) { self.postcode(data.Postcode); };
					if( data.Woonplaats !== null ) { self.woonplaats(data.Woonplaats); };
				} else {
					fotolab.vm.flashmessage.addMessage("Klantcode bestaat niet!", "danger");
					fotolab.vm.reset();
				}
			});
		}
	};

	var saveKlantDetails = function() {
		if( fotolab.vm.validations.validateKlantGegevens() ) {
            $.ajax({
                type: "PUT",
                url: Info.baseApiUrl + "api/klant/" + self.key(),
                data: {
                	Key: self.key(),
                	Naam: self.naam(),
                	Straat: self.straat(),
                	Huisnummer: self.huisnummer(),
                	Postcode: self.postcode(),
                	Woonplaats: self.woonplaats()
                },
                //dataType: "json",
                success: function (data) {
                    fotolab.vm.flashmessage.addMessage("Uw gegevens zijn opgeslagen!", "success");
                },
                error: function (data) {
                    fotolab.vm.flashmessage.addMessage("Klant kon niet opgeslagen worden!", "danger");
                }
            })
		}
	};

	self.reset = function() {
		self.key("");
		self.fotoseries.removeAll();


		self.naam("");
		self.straat("");
		self.huisnummer("");
		self.postcode("");
		self.woonplaats("");
	};

	self.key.subscribe(getKlantDetails);
	self.naam.subscribe(getFotoseries);

	self.naam.subscribe(saveKlantDetails);
	self.straat.subscribe(saveKlantDetails);
	self.huisnummer.subscribe(saveKlantDetails);
	self.postcode.subscribe(saveKlantDetails);
	self.woonplaats.subscribe(saveKlantDetails);
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

	self.validations = {
		validateLogin: function() {
			return ( 
				(self.klant.key().length > 0) && 
				(self.klant.naam().length > 0)
			);
		},
		validateKlantGegevens: function() {
			return (
				(self.klant.naam().length > 0) &&
				(self.klant.straat().length > 0) &&
				(self.klant.huisnummer().length > 0) &&
				(self.klant.postcode().length > 0) &&
				(self.klant.woonplaats().length > 0)
			);
		}
	};

	self.sections = ko.observableArray([
		new Section("fotolab_login", "Login", self.validations.validateLogin ),
		new Section("fotolab_klantgegevens", "Klantgegevens", self.validations.validateKlantGegevens ),
		new Section("fotolab_select_fotoserie", "Selecteer fotoserie", function() { return true; } ),
		new Section("fotolab_select_fotos", "Selecteer foto's", function() { return true; } ),
		new Section("fotolab_productlist", "Productenlijst", function() { return true; } ),
		new Section("fotolab_payment", "Betaalmethode", function() { return true; } ),
		new Section("fotolab_thanks", "Bedankt voor uw bestelling!", function() { return true; } )
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

var fotolab = { vm: new fotolabViewModel() };

$(document).ready(function() {
	ko.applyBindings(fotolab.vm);
});

