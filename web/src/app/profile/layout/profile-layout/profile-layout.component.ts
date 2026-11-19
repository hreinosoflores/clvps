import { Component, computed, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatDividerModule } from '@angular/material/divider';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-profile-layout',
  imports: [RouterModule, MatDividerModule, CommonModule],
  templateUrl: './profile-layout.component.html',
  styleUrl: './profile-layout.component.css'
})
export class ProfileLayoutComponent {
  private authService = inject(AuthService);

  usuarioActual = computed(() => this.authService.usuarioActual());
}
