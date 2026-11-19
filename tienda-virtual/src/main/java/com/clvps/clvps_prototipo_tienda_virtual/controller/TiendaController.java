package com.clvps.clvps_prototipo_tienda_virtual.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;

import com.clvps.clvps_prototipo_tienda_virtual.ApplicationProvider;
import com.clvps.clvps_prototipo_tienda_virtual.model.ItemCarrito;
import com.clvps.clvps_prototipo_tienda_virtual.model.Videojuego;

import jakarta.servlet.http.HttpSession;

import java.util.ArrayList;
import java.util.List;

@Controller
public class TiendaController {	

    @Autowired
    private ApplicationProvider provider;

	private static final List<Videojuego> videojuegos = new ArrayList<>();

	static {
		videojuegos.add(new Videojuego(1L, "/images/black_myth_wu_kong-5858054.jpg", "Black Myth Wukong", 225.50));
		videojuegos
				.add(new Videojuego(2L, "/images/ecf404c4cded91e164f3133c64d74e77.jpg", "God Of War Ragnarök", 345.00));
		videojuegos.add(new Videojuego(3L, "/images/grand_theft_auto_vi-5854699.jpg", "Grand Theft Auto VI", 399.90));
		videojuegos.add(
				new Videojuego(4L, "/images/Kingdom_Come_Deliverance_II.jpg", "Kingdom Come Deliverance II", 125.60));
		videojuegos.add(new Videojuego(5L, "/images/ORRRfEnDj_600x600__1.jpg", "FC 25", 99.90));
		videojuegos.add(new Videojuego(6L, "/images/silent-hill-2-remake-202410813414824_13.jpg",
				"Silent Hill 2 Remake", 150.40));
		videojuegos.add(new Videojuego(7L, "/images/33.jpg", "Cyberpunk 2077", 115.00));
	}

	@GetMapping("/")
	public String listarProductos(Model model, HttpSession session) {
		model.addAttribute("videojuegos", videojuegos);
		model.addAttribute("titulo", "Tienda Virtual");
		model.addAttribute("subtitulo", "Tu Game Store Favorita");

		@SuppressWarnings("unchecked")
		List<ItemCarrito> carrito = (List<ItemCarrito>) session.getAttribute("carrito");
		int xCarrito = 0;
		if (carrito != null) {
			for (ItemCarrito ic : carrito) {
				xCarrito += ic.getCantidad();
			}
		}
		model.addAttribute("xCarrito", xCarrito);
		return "videojuegos";
	}

	@PostMapping("/add-to-cart")
	public String addToCart(@RequestParam Long id, HttpSession session) {
		@SuppressWarnings("unchecked")
		List<ItemCarrito> carrito = (List<ItemCarrito>) session.getAttribute("carrito");
		if (carrito == null) {
			carrito = new ArrayList<>();
		}

		boolean itemFound = false;
		for (ItemCarrito ic : carrito) {
			if (ic.getVideojuego().getId().equals(id)) {
				ic.setCantidad(ic.getCantidad() + 1);
				itemFound = true;
				break;
			}
		}

		if (!itemFound) {
			for (Videojuego vj : videojuegos) {
				if (vj.getId().equals(id)) {
					carrito.add(new ItemCarrito(vj, 1));
					break;
				}
			}
		}

		session.setAttribute("carrito", carrito);
		return "redirect:/";
	}

	@GetMapping("/cart")
	public String mostrarCarrito(Model model, HttpSession session) {
		@SuppressWarnings("unchecked")
		List<ItemCarrito> carrito = (List<ItemCarrito>) session.getAttribute("carrito");
		model.addAttribute("carrito", carrito);
		model.addAttribute("titulo", "Tienda Virtual");
		model.addAttribute("subtitulo", "Tu Game Store Favorita");
		

		double totalCarrito = 0;
		if (carrito != null) {
			for (ItemCarrito ic : carrito) {
				totalCarrito += ic.getVideojuego().getPrecioDescontado() * ic.getCantidad();
			}
		}

		model.addAttribute("totalCarrito", totalCarrito);		
        model.addAttribute("clvpsUrl", getCLVPSURL(totalCarrito));
		return "carrito";
	}

	@PostMapping("/delete-from-cart")
	public String deleteFromCart(@RequestParam Long id, HttpSession session) {
		@SuppressWarnings("unchecked")
		List<ItemCarrito> carrito = (List<ItemCarrito>) session.getAttribute("carrito");
		if (carrito != null) {
			for (ItemCarrito ic : carrito) {
				if (ic.getVideojuego().getId().equals(id)) {
					carrito.remove(ic);
					break;
				}
			}
		}

		if(carrito.isEmpty()) {
			session.removeAttribute("carrito");
		} else {
			session.setAttribute("carrito", carrito);			
		}
		return "redirect:/cart";
	}
	
	
	private String getCLVPSURL(double total) {		
		String totalStr = String.valueOf(Math.round(total * 100.0));
		return String.format("%s/pago/redirect?vendedor=%s&importe=%s", provider.getClvpsUrl(), "tiendavirtualsac", totalStr);
	}

}
