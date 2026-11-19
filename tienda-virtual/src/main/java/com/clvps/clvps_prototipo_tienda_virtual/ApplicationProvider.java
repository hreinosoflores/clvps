package com.clvps.clvps_prototipo_tienda_virtual;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

@Component
public class ApplicationProvider {
	@Value("${clvps.url}")
	private String clvpsUrl;

	public String getClvpsUrl() {
		return clvpsUrl;
	}
}
