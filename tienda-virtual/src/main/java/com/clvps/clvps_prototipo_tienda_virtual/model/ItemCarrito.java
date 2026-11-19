package com.clvps.clvps_prototipo_tienda_virtual.model;

public class ItemCarrito {
	private Videojuego videojuego;
	private int cantidad;
	private double total;

	public ItemCarrito(Videojuego videojuego, int cantidad) {
		this.videojuego = videojuego;
		this.cantidad = cantidad;
		this.total = videojuego.getPrecioDescontado() * cantidad;
	}

	public Videojuego getVideojuego() {
		return videojuego;
	}

	public int getCantidad() {
		return cantidad;
	}

	public double getTotal() {
		return total;
	}

	public void setCantidad(int cantidad) {
		this.cantidad = cantidad;
		this.total = videojuego.getPrecioDescontado() * cantidad;
	}
}
