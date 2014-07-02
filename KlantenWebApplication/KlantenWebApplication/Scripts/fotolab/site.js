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

ko.bindingHandlers.lightgreenbackground = {
    init: function(element, valueAccessor) {
        // Initially set the element to be instantly visible/hidden depending on the value
        var value = valueAccessor();
        if( ko.unwrap(value) ) {
        	$(element).css('background-color', 'LightGreen')
        } else {
        	$(element).css('background-color', '')
        }
    },
    update: function(element, valueAccessor) {
        // Whenever the value subsequently changes, slowly fade the element in or out
        var value = valueAccessor();
        if( ko.unwrap(value) ) {
        	$(element).css('background-color', 'LightGreen')
        } else {
        	$(element).css('background-color', '')
        }
    }
};

ko.bindingHandlers.redborder = {
    init: function(element, valueAccessor) {
        // Initially set the element to be instantly visible/hidden depending on the value
        var value = valueAccessor();
        if( ko.unwrap(value) ) {
        	$(element).css('border-color', 'red')
        } else {
        	$(element).css('border-color', '')
        }
    },
    update: function(element, valueAccessor) {
        // Whenever the value subsequently changes, slowly fade the element in or out
        var value = valueAccessor();
        if( ko.unwrap(value) ) {
        	$(element).css('border-color', 'red')
        } else {
        	$(element).css('border-color', '')
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

ko.bindingHandlers.data_key = {
    update: function (element, valueAccessor) {
        ko.bindingHandlers.attr.update(element, function () {
            return { 'data-key': valueAccessor()}
        });
    }
};

ko.bindingHandlers.title = {
    update: function (element, valueAccessor) {
        ko.bindingHandlers.attr.update(element, function () {
            return { 'title': valueAccessor()}
        });
    }
};

var Utility = {
	random: function(min, max) {
		if(typeof min === "undefined") { min = 0; }
		if(typeof max === "undefined") { max = 10; }

		return Math.floor( Math.random() * ( max - min + 1 ) + min);
	},
	arrayRemoveAt: function(arr, key) {
		return arr.splice(key, 1);
	}
};


var Info = {
	baseApiUrl: "http://localhost:2372/",
	payPalReturnUrl: this.baseApiUrl + "Home/PaypalEnd/"
};

// usage: self.flashmessage.addMessage("Dit is een message");
// Als er geen message is, dan is de message 'm', dit om de hoogte van de div te houden
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
}

function PayPalPayment(order) {
	var self = this;

	self.order = order;

	self.approveUrl = ko.observable("");

	self.initiatePayment = function() {
		var sendObject = {
        	Id: 0,
        	Datum: new Date(),
        	BestelRegels: [],
        	BetaalMethod: {
        		RedirectUrl: Info.payPalReturnUrl
        	}
       	};
       	for (var i = 0; i < self.order.items().length; i++) {
       		var item = self.order.items()[i];
       		var ft = item.foto;
       		var pr = item.fotoproduct();

       		sendObject.BestelRegels.push({ 
       			Foto: { Id: ft.id, Bedrag: ft.bedrag },
       			FotoProduct: { Id: pr.id, Naam: pr.naam, Meerprijs: pr.bedrag }
       		});
       	};


        $.ajax({
            type: "POST",
            url: Info.baseApiUrl + "api/klant/" + self.order.klant.key() +"/bestelling/",
        	data: {"": JSON.stringify(sendObject) },
            success: function (data) {
                
            },
            error: function (data) {
                
            },
            timeout: 10000 // extra lange timeout
        })
	};
}

function FotoProduct(id, naam, bedrag) {
	var self = this;
	self.id = id;
	self.naam = naam;
	self.bedrag = bedrag;
}

function Item(foto) {
	var self = this;

	self.foto = foto;
	self.fotoproduct = ko.observable(new FotoProduct("Origineel", 0));
	self.aantal = ko.observable(1);

	self.subtotal = ko.computed(function() {
		return (self.foto.bedrag + self.fotoproduct().bedrag) * self.aantal();
	});

	self.formattedSubTotal = ko.computed(function() {
		return "€ " + (self.subtotal()).toFixed(2);
	});
}

function Order(klant) {
	var self = this;

	self.payment = new PayPalPayment(self);
	self.klant = klant;
	self.items = ko.observableArray([]);
	self.isPaid = ko.observable(false);

	self.availableFotoProducten = ko.observableArray([]);

	self.selectFoto = function(foto) {
		if( foto.isSelected() ) {
			foto.isSelected(false);
			self.items.remove(function(item) { return item.foto.isSelected() == false });
		} else {
			var item = new Item(foto);
			self.items.push(item);
			foto.isSelected(true);
		}
	};

	self.total = ko.computed(function() {
		var total = 0;
		$.each(self.items(), function() { total += this.subtotal(); });
		return total;
	});

	self.formattedTotal = ko.computed(function() {
		return "€ " + (self.total()).toFixed(2);
	});

	self.reset = function() {
		self.items.removeAll();
	};

	self.getAvailableFotoProducten = function() {
		$.getJSON(Info.baseApiUrl + "api/fotoproducten", function(dataArr) {
			var fotoproducten = $.map(dataArr, function(datarow) {
				return new FotoProduct(datarow.Id, datarow.Naam, datarow.Meerprijs);
			});
			self.availableFotoProducten(fotoproducten);
		});
	};



	self.getAvailableFotoProducten();
}

function Foto(id, bedrag) {
	var self = this;

	self.id = id;
	self.bedrag = bedrag;
	self.fotoserie = null;
	self.isSelected = ko.observable(false);

	self.formattedBedrag = function() {
		return "€ " + self.bedrag.toFixed(2);
	};
}

function Fotoserie(key, naam, datum, fotos) {
	var self = this;

	self.key = key;
	self.naam = naam;
	self.datum = datum;
	self.fotos = fotos;
	self.isActive = ko.observable(false);

	self.getFotoSrc = function(id) {
		return Info.baseApiUrl + "api/fotoserie/" + self.key + "/foto/" + id;
	};

	self.randomFotoSrc = ko.computed(function() {
		if( self.fotos.length > 0) {
			var fotosKey = Utility.random(0, self.fotos.length - 1);
			var id = self.fotos[fotosKey].id;
			return self.getFotoSrc(id);
		}
		return "/Content/Images/Circle-question-red.svg";
	});

	self.bindFotoserieToFotos = function() {
		for (var i = 0; i < self.fotos.length; i++) {
			self.fotos[i].fotoserie = self;
		};
	};
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
				var fss = $.map(dataArr, function(datarow) {

					var fotos = [];
					for (var i = 0; i < datarow.Fotos.length; i++) {
						var foto = new Foto(datarow.Fotos[i].Id, datarow.Fotos[i].Bedrag); // TODO: Prijs aan een foto geven vanuit de WEBAPI
						fotos.push(foto);
					};
					var fotoserie = new Fotoserie(datarow.Key, datarow.Naam, datarow.Datum, fotos);
					fotoserie.bindFotoserieToFotos();
					return fotoserie;
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

	// Om een JSON string te POSTen in ASP NET moet je geen JSON contentType meegeven.
	// Ook moet je als data een object met een naamloze-parameter meegegeven. Deze parameter kan dan de JSON string bevatten
	var saveKlantDetails = function() {
		if( fotolab.vm.validations.validateKlantGegevens() ) {
            $.ajax({
                type: "PUT",
                url: Info.baseApiUrl + "api/klant/" + self.key(),
             //   dataType: 'json',
              //  contentType: "application/json",
                /*data: JSON.stringify({
                	Id: 0,
                	Naam: self.naam(),
                	Klant_key: self.key(),
                	Straat: self.straat(),
                	Huisnummer: self.huisnummer(),
                	Postcode: self.postcode(),
                	Woonplaats: self.woonplaats()
                }),*/
            	data: { "": JSON.stringify({
                	Id: 0,
                	Naam: self.naam(),
                	Klant_key: self.key(),
                	Straat: self.straat(),
                	Huisnummer: self.huisnummer(),
                	Postcode: self.postcode(),
                	Woonplaats: self.woonplaats()
                }) },
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

function fotolabViewModel() {
	var self = this;

	self.flashmessage = new FlashMessages();

	self.klant = new Klant();
	self.order = new Order(self.klant);
	self.activeFotoserie = ko.observable();

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
		},
		validateSelecteerFotoserie: function() {
			return ( self.activeFotoserie() !== null && typeof self.activeFotoserie() !== "undefined");
		},
		validateSelecteerFotos: function() {
			return (self.order.items().length > 0);
		},
		validateSelecteerBetaalMethode: function() {
			return self.order.isPaid();
		},
	};

	self.sections = ko.observableArray([
		new Section("fotolab_login", "Login", self.validations.validateLogin ),
		new Section("fotolab_klantgegevens", "Klantgegevens", self.validations.validateKlantGegevens ),
		new Section("fotolab_select_fotoserie", "Selecteer fotoserie", self.validations.validateSelecteerFotoserie ),
		new Section("fotolab_select_fotos", "Selecteer foto's", self.validations.validateSelecteerFotos ),
		new Section("fotolab_productlist", "Overzicht Producten", function() { return true; } ),
		new Section("fotolab_payment", "Selecteer Betaalmethode", self.validations.validateSelecteerBetaalMethode ),
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
		self.activeFotoserie(null);
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

	self.setActiveFotoserie = function(fotoserie) {
		var oldFotoserie = self.activeFotoserie();
		if( oldFotoserie !== null && typeof oldFotoserie !== "undefined") {
			oldFotoserie.isActive(false);
		}

		self.activeFotoserie(fotoserie);
		fotoserie.isActive(true);
	};

	/*self.isActiveFotoserie = ko.computed(function(fotoserieKey) {
		console.log("BEGIN isActiveFotoserie");
		console.dir(self.activeFotoserie());
		console.dir(fotoserieKey);
		console.log("END isActiveFotoserie");
		return (self.activeFotoserie() !== null && 
				typeof self.activeFotoserie() !== "undefined" &&
				self.activeFotoserie().key == fotoserieKey);
	});*/
}

var fotolab = { vm: new fotolabViewModel() };

$(document).ready(function() {
	ko.applyBindings(fotolab.vm);
});

