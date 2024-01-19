////////////////////////////////////////////////////
// Theme Name:  - 
// Theme URI:   - 
// Description: - 
// Author:      - Adem Duran - 
// Author URI:  - https://bionluk.com/adeemdrn - 
// Version:     - 1.0 - 
////////////////////////////////////////////////////
////////////////////////////////////////////////////
// 01. Right Click Blocking on Site JS
// var isNS = (navigator.appName == "Netscape") ? 1 : 0;
// var EnableRightClick = 0;
// if(isNS)
// document.captureEvents(Event.MOUSEDOWN||Event.MOUSEUP);
// function mischandler(){
// if(EnableRightClick==1){ return true; }
// else {return false; }
// }
// function mousehandler(e){
// if(EnableRightClick==1){ return true; }
// var myevent = (isNS) ? e : event;
// var eventbutton = (isNS) ? myevent.which : myevent.button;
// if((eventbutton==2)||(eventbutton==3)) return false;
// }
// function keyhandler(e) {
// var myevent = (isNS) ? e : window.event;
// if (myevent.keyCode==96)
// EnableRightClick = 1;
// return;
// }
// document.oncontextmenu = mischandler;
// document.onkeypress = keyhandler;
// document.onmousedown = mousehandler;
// document.onmouseup = mousehandler;
//-->

////////////////////////////////////////////////////
// 0. Sticky Menu JS
// Lazy Load Images using Intersection Observer
$("#loader").delay(2500).slideToggle("slow");




var swiper = new Swiper(".mySwiperProduct", {
	slidesPerView: 3,
	spaceBetween: 20,
	pagination: {
		el: ".swiper-pagination",
		clickable: true,
	},
	speed: 550,
	autoplay: {
		delay: 6000,
		disableOnInteraction: false
	},
	breakpoints: {
		// when window width is >= 320px
		320: {
			slidesPerView: 2,
		},
		// when window width is >= 480px
		480: {
			slidesPerView: 2,
		},
		// when window width is >= 640px
		640: {
			slidesPerView: 2,
		},
		// when window width is >= 640px
		991: {
			slidesPerView: 3,
		}
	}
});


var swiper = new Swiper(".mySwiperBenkien", {
	loop: true,
	centeredSlides: true,
	slidesPerView: 3,
	spaceBetween: 20,
	speed: 550,
	autoplay: {
		delay: 6000,
		disableOnInteraction: false
	},
	centeredSlides: true,
	watchOverflow: true,
	watchSlidesProgress: true,
	updateOnWindowResize: true,
	breakpoints: {
		// when window width is >= 320px
		320: {
			slidesPerView: 2,
		},
		// when window width is >= 480px
		480: {
			slidesPerView: 2,
		},
		// when window width is >= 640px
		640: {
			slidesPerView: 2,
		},
		// when window width is >= 640px
		991: {
			slidesPerView: 3,
		}
	}
});

var swiper = new Swiper(".mySwiperOnes", {
	loop: true,
	centeredSlides: true,
	slidesPerView: 3,
	spaceBetween: 20,
	speed: 550,
	autoplay: {
		delay: 6000,
		disableOnInteraction: false
	},
	centeredSlides: true,
	watchOverflow: true,
	watchSlidesProgress: true,
	updateOnWindowResize: true,
	breakpoints: {
		// when window width is >= 320px
		320: {
			slidesPerView: 2,
		},
		// when window width is >= 480px
		480: {
			slidesPerView: 2,
		},
		// when window width is >= 640px
		640: {
			slidesPerView: 2,
		},
		// when window width is >= 640px
		991: {
			slidesPerView: 3,
		}
	}
});
var swiper = new Swiper(".mySwiperPopulary", {
	loop: true,
	centeredSlides: true,
	slidesPerView: 3,
	spaceBetween: 10,
	speed: 450,
	autoplay: {
		delay: 6000,
		disableOnInteraction: false
	},
	centeredSlides: true,
	watchOverflow: true,
	watchSlidesProgress: true,
	updateOnWindowResize: true,
	breakpoints: {
		// when window width is >= 320px
		320: {
			slidesPerView: 2,
		},
		// when window width is >= 480px
		480: {
			slidesPerView: 2,
		},
		// when window width is >= 640px
		640: {
			slidesPerView: 2,
		},
		// when window width is >= 640px
		991: {
			slidesPerView: 2,
		}
	}
});
var swiper = new Swiper(".mySwiperShopItems", {
	loop: true,
	spaceBetween: 10,
	speed: 450,
	autoplay: {
		delay: 4000,
		disableOnInteraction: false
	},
	breakpoints: {
		// when window width is >= 320px
		320: {
			slidesPerView: 1,
		},
		// when window width is >= 480px
		480: {
			slidesPerView: 1,
		},
		// when window width is >= 640px
		640: {
			slidesPerView: 2,
		},
		// when window width is >= 640px
		991: {
			slidesPerView: 4,
		}
	}
});




const progressCircle = document.querySelector(".autoplay-progress svg");
const progressContent = document.querySelector(".autoplay-progress span");
var swiper = new Swiper(".mySwiper", {
	spaceBetween: 30,
	centeredSlides: true,
	autoplay: {
		delay: 6000,
		disableOnInteraction: false
	},
	speed: 1200,
	grabCursor: true,
	effect: "creative",
	creativeEffect: {
		prev: {
			shadow: true,
			translate: ["-120%", 0, -500],
		},
		next: {
			shadow: true,
			translate: ["120%", 0, -500],
		},
	},
	on: {
		autoplayTimeLeft(s, time, progress) {
			progressCircle.style.setProperty("--progress", 1 - progress);
			progressContent.textContent = `${Math.ceil(time / 1300)}s`;
		}
	}
});


const baseline = document.querySelector('.base-headline');
const line = document.querySelector('.animated-headline');
const start = line.textContent.split('');
const end = baseline.textContent.split('');
const length = end.length;

gsap.registerPlugin(ScrollTrigger);
gsap.to('.animation', {
	scrollTrigger: {
		trigger: '.animation',
		start: 'center 90%',
		end: 'center 35%',
		onUpdate: tween => update(tween.progress)
	}
});

function update(progress) {
	const offset = Math.round(progress * length);
	line.innerHTML = end.slice(0, offset).join('') + start.slice(offset, start.length).join('');
}



document.addEventListener("DOMContentLoaded", function () {
	const options = {
		root: null,
		rootMargin: "0px",
		threshold: 0.4
	};

	// IMAGE ANIMATION

	let revealCallback = (entries) => {
		entries.forEach((entry) => {
			let container = entry.target;

			if (entry.isIntersecting) {
				console.log();
				container.classList.add("animating");
				return;
			}

			if (entry.boundingClientRect.top > 0) {
				container.classList.remove("animating");
			}
		});
	};

	let revealObserver = new IntersectionObserver(revealCallback, options);

	document.querySelectorAll(".reveal").forEach((reveal) => {
		revealObserver.observe(reveal);
	});

	// // TEXT ANIMATION

	// let fadeupCallback = (entries) => {
	//   entries.forEach((entry) => {
	//     let container = entry.target;
	//     container.classList.add("not-fading-up");

	//     if (entry.isIntersecting) {
	//       container.classList.add("fading-up");
	//       return;
	//     }

	//     if (entry.boundingClientRect.top > 0) {
	//       container.classList.remove("fading-up");
	//     }
	//   });
	// };

	// let fadeupObserver = new IntersectionObserver(fadeupCallback, options);

	document.querySelectorAll(".fadeup").forEach((fadeup) => {
		fadeupObserver.observe(fadeup);
	});
});



(function () {
	"use strict";
	var jQueryPlugin = (window.jQueryPlugin = function (ident, func) {
		return function (arg) {
			if (this.length > 1) {
				this.each(function () {
					var $this = $(this);

					if (!$this.data(ident)) {
						$this.data(ident, func($this, arg));
					}
				});

				return this;
			} else if (this.length === 1) {
				if (!this.data(ident)) {
					this.data(ident, func(this, arg));
				}

				return this.data(ident);
			}
		};
	});
})();

(function () {
	"use strict";
	function Guantity($root) {
		const element = $root;
		const quantity = $root.first("data-quantity");
		const quantity_target = $root.find("[data-quantity-target]");
		const quantity_minus = $root.find("[data-quantity-minus]");
		const quantity_plus = $root.find("[data-quantity-plus]");
		var quantity_ = quantity_target.val();
		$(quantity_minus).click(function () {
			quantity_target.val(--quantity_);
		});
		$(quantity_plus).click(function () {
			quantity_target.val(++quantity_);
		});
	}
	$.fn.Guantity = jQueryPlugin("Guantity", Guantity);
	$("[data-quantity]").Guantity();
})();
