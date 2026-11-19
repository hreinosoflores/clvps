import { Component, computed, EventEmitter, inject, Output } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { AvatarComponent } from "../avatar/avatar.component";
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../services/auth.service';


@Component({
  selector: 'app-navbar',
  imports: [MatIconModule, MatButtonModule, AvatarComponent, CommonModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  private authService = inject(AuthService);

  loginStatus = computed(() => this.authService.loginStatus());

  private router = inject(Router);

  goToLogin() {
    this.router.navigate(['/auth']);
  }

  @Output() toggleSidenav = new EventEmitter<void>();

  onToggleSidenav() {
    this.toggleSidenav.emit();
  }
}
