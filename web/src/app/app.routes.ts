import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/pages/home/home.component';
import { LoginComponent } from './auth/pages/login/login.component';
import { ProfileLayoutComponent } from './profile/layout/profile-layout/profile-layout.component';
import { DatosBasicosComponent } from './profile/pages/datos-basicos/datos-basicos.component';
import { TransaccionesComponent } from './profile/pages/transacciones/transacciones.component';
import { OrdenesPagoComponent } from './profile/pages/ordenes-pago/ordenes-pago.component';
import { SolicitudesRetiroComponent } from './profile/pages/solicitudes-retiro/solicitudes-retiro.component';
import { TransaccionPagoDetalleComponent } from './profile/pages/transacciones/transaccion-pago-detalle/transaccion-pago-detalle.component';
import { TransaccionRecargaDetalleComponent } from './profile/pages/transacciones/transaccion-recarga-detalle/transaccion-recarga-detalle.component';
import { TransaccionRetiroDetalleComponent } from './profile/pages/transacciones/transaccion-retiro-detalle/transaccion-retiro-detalle.component';
import { RedirectComponent } from './pagos/pages/redirect/redirect.component';
import { DetalleComponent } from './pagos/pages/detalle/detalle.component';
import { ConfirmacionComponent } from './pagos/pages/confirmacion/confirmacion.component';
import { RecargaComponent } from './profile/pages/recarga/recarga.component';

export const routes: Routes = [
  {
    path: 'auth',
    component: LoginComponent
  },
  {
    path: 'profile',
    component: ProfileLayoutComponent,
    children: [
      {
        path: 'mis-datos', component: DatosBasicosComponent
      },
      {
        path: 'mis-transacciones',
        component: TransaccionesComponent,

      },
      {
        path: 'mis-transacciones',
        children: [
          {
            path: 'pagos/:id', component: TransaccionPagoDetalleComponent
          },
          {
            path: 'recargas/:id', component: TransaccionRecargaDetalleComponent
          },
          {
            path: 'retiros/:id', component: TransaccionRetiroDetalleComponent
          },
        ]
      },
      {
        path: 'mis-ordenes-de-pago', component: OrdenesPagoComponent
      },
      {
        path: 'mis-solicitudes-de-retiro', component: SolicitudesRetiroComponent
      },
      {
        path: 'recarga',
        component: RecargaComponent,

      },
      {
        path: '',
        redirectTo: 'mis-datos',
        pathMatch: 'full',
      },
      {
        path: '**',
        redirectTo: 'mis-datos',
      },
    ]
  },
  {
    path: 'pago',
    children: [
      {
        path: 'redirect', component: RedirectComponent
      },
      {
        path: 'detalle', component: DetalleComponent
      },
      {
        path: 'confirmacion', component: ConfirmacionComponent
      },
    ]
  },
  {
    path: '',
    component: HomeComponent
  }
];
