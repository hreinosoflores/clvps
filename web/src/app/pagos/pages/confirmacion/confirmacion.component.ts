import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PagoConfirmacionResponse } from '../../../models/PagoConfirmacionResponse';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-confirmacion',
  imports: [CommonModule, MatIconModule, MatButtonModule],
  templateUrl: './confirmacion.component.html',
  styleUrl: './confirmacion.component.css'
})
export class ConfirmacionComponent implements OnInit {
  private router = inject(Router);

  confirmacionData?: PagoConfirmacionResponse;

  constructor() {
    const navigation = this.router.getCurrentNavigation();
    this.confirmacionData = navigation?.extras.state?.['data'];
  }

  ngOnInit(): void {
    console.log(this.confirmacionData);
  }

  maskEmail(email?: string): string {
    if (!email) return '';
    const [user, domain] = email.split('@');
    return `${user[0]}***@${domain}`;
  }

  irIndex() {
    this.router.navigate(['/']);
  }
}
