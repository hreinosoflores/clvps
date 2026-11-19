package com.clvps.clvps_prototipo_tienda_virtual.model;

public class Videojuego {
	private Long id;
	private String imagenUrl;
	private String titulo;
	private double precio;
	private double precioDescontado;

	public Videojuego(Long id, String imagenUrl, String titulo, double precio) {
		this.id = id;
		this.imagenUrl = imagenUrl;
		this.titulo = titulo;
		this.precio = precio;
		this.precioDescontado = precio * 0.85;
	}

	public Long getId() {
		return id;
	}

	public void setId(Long id) {
		this.id = id;
	}

	public String getImagenUrl() {
		return imagenUrl;
	}

	public void setImagenUrl(String imagenUrl) {
		this.imagenUrl = imagenUrl;
	}

	public String getTitulo() {
		return titulo;
	}

	public void setTitulo(String titulo) {
		this.titulo = titulo;
	}

	public double getPrecio() {
		return precio;
	}

	public void setPrecio(double precio) {
		this.precio = precio;
	}
	
	public double getPrecioDescontado() {
		return precioDescontado;
	}

}
