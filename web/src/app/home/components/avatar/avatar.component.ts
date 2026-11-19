import { Component, computed, EventEmitter, inject, Output } from '@angular/core';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-avatar',
  imports: [],
  templateUrl: './avatar.component.html',
  styleUrl: './avatar.component.css'
})
export class AvatarComponent {

  private authService = inject(AuthService);

  usuarioActual = computed(() => this.authService.usuarioActual());

  getInicial() {
    return this.usuarioActual()?.nombreCompleto!.charAt(0).toUpperCase();
  }

}
