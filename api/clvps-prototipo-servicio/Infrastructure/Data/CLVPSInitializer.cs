using clvps_prototipo_servicio.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace clvps_prototipo_servicio.Infrastructure.Data
{
    public static class CLVPSInitializer
    {
        public static async Task Initialize(CLVPSContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            IdentityResult result;

            if (!await roleManager.RoleExistsAsync("Buyer"))
            {
                result = await roleManager.CreateAsync(new IdentityRole("Buyer"));
                if (!result.Succeeded)
                {
                    MostrarErrores(result);
                }
            }

            if (!await roleManager.RoleExistsAsync("Seller"))
            {
                result = await roleManager.CreateAsync(new IdentityRole("Seller"));
                if (!result.Succeeded)
                {
                    MostrarErrores(result);
                }
            }

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                result = await roleManager.CreateAsync(new IdentityRole("Admin"));
                if (!result.Succeeded)
                {
                    MostrarErrores(result);
                }
            }

            //Comprador
            if (await userManager.FindByNameAsync("hreinoso") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "hreinoso",
                    Email = "hreinoso@gmail.com",
                    PhoneNumber = "944809183",
                };

                result = await userManager.CreateAsync(user, "Pocho2000!");

                if (result.Succeeded)
                {
                    Debug.WriteLine("Usuario creado");
                    result = await userManager.AddToRoleAsync(user, "Buyer");
                    if (result.Succeeded)
                    {
                        Debug.WriteLine("Rol Asignado");
                    }
                    else
                    {
                        MostrarErrores(result);
                    }
                }
                else
                {
                    MostrarErrores(result);
                }
            }
            else
            {
                Debug.WriteLine("El usuario ya existe");
            }

            var userBuyer = await userManager.FindByNameAsync("hreinoso");

            if (await context.Buyers.FindAsync(userBuyer!.Id) == null)
            {
                var buyer = new Buyer
                {
                    Id = userBuyer!.Id,
                    Nombres = "Humberto",
                    Apellidos = "Reinoso",
                    DNI = "73580887"
                };
                await context.Buyers.AddAsync(buyer);
                await context.SaveChangesAsync();
            }
            else
            {
                Debug.WriteLine("El comprador ya existe");
            }


            //Vendedor
            if (await userManager.FindByNameAsync("tiendavirtualsac") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "tiendavirtualsac",
                    Email = "soporte@tiendavirtual.com",
                    PhoneNumber = "809-555-5555",
                };

                result = await userManager.CreateAsync(user, "Pocho2000!");

                if (result.Succeeded)
                {
                    Debug.WriteLine("Usuario creado");
                    result = await userManager.AddToRoleAsync(user, "Seller");
                    if (result.Succeeded)
                    {
                        Debug.WriteLine("Rol Asignado");
                    }
                    else
                    {
                        MostrarErrores(result);
                    }
                }
                else
                {
                    MostrarErrores(result);
                }
            }
            else
            {
                Debug.WriteLine("El usuario ya existe");
            }

            var userSeller = await userManager.FindByNameAsync("tiendavirtualsac");

            if (await context.Sellers.FindAsync(userSeller!.Id) == null)
            {
                var seller = new Seller
                {
                    Id = userSeller!.Id,
                    Raz_social = "Tienda Virtual SAC",
                    RUC = "20123456789"
                };
                await context.Sellers.AddAsync(seller);
                await context.SaveChangesAsync();
            }
            else
            {
                Debug.WriteLine("El vendedor ya existe");
            }


            // Cuentas virtuales         
            if (await context.VirtualAccounts.FirstOrDefaultAsync(va => va.Id_user == userBuyer!.Id) == null)
            {
                var virtualAccount = new VirtualAccount
                {
                    Id = 1,
                    Id_user = userBuyer!.Id,
                    Nro_cuenta = "93181478944686808030",
                    Saldo_soles = 1000,
                    Saldo_dolares = 276.55
                };
                await context.VirtualAccounts.AddAsync(virtualAccount);
                await context.SaveChangesAsync();
            }
            else
            {
                Debug.WriteLine("La cuenta virtual ya existe");
            }

            if (await context.VirtualAccounts.FirstOrDefaultAsync(va => va.Id_user == userSeller!.Id) == null)
            {
                var virtualAccount = new VirtualAccount
                {
                    Id = 1,
                    Id_user = userSeller!.Id,
                    Nro_cuenta = "46959435239923108559",
                    Saldo_soles = 12000,
                    Saldo_dolares = 3318.59
                };
                await context.VirtualAccounts.AddAsync(virtualAccount);
                await context.SaveChangesAsync();
            }
            else
            {
                Debug.WriteLine("La cuenta virtual ya existe");
            }


            // Orden de Pago
            if (await context.OrdenesPago.FirstOrDefaultAsync(op => op.Id_b == userBuyer!.Id && op.Id_s == userSeller!.Id) == null)
            {
                var ordenPago = new OrdenPago
                {
                    Monto = 1450.45,
                    Estado = "O",
                    Id_b = userBuyer!.Id,
                    Id_s = userSeller!.Id
                };
                await context.OrdenesPago.AddAsync(ordenPago);
                await context.SaveChangesAsync();
            }
            else
            {
                Debug.WriteLine("La orden de pago ya existe");
            }

            // Transacciones de Pago
            var op = await context.OrdenesPago.FirstOrDefaultAsync(op => op.Id_b == userBuyer!.Id && op.Id_s == userSeller!.Id);

            var vaBuyer = await context.VirtualAccounts.FirstOrDefaultAsync(va => va.Id_user == userBuyer!.Id);

            if (await context.TransactionsPago.FirstOrDefaultAsync(tr => tr.Id_user == userBuyer!.Id && tr.Id_ord == op!.Id) == null)
            {
                var transactionPagoBuyer = new TransactionPago
                {
                    Id_va = vaBuyer!.Id,
                    Id_user = userBuyer!.Id,
                    Id_ord = op!.Id,
                    Tipo = "1",
                    Monto = 1450.45
                };
                await context.TransactionsPago.AddAsync(transactionPagoBuyer);
                await context.SaveChangesAsync();
            }
            else
            {
                Debug.WriteLine("La transacción ya existe");
            }

            var vaSeller = await context.VirtualAccounts.FirstOrDefaultAsync(va => va.Id_user == userSeller!.Id);

            if (await context.TransactionsPago.FirstOrDefaultAsync(tr => tr.Id_user == userSeller!.Id && tr.Id_ord == op!.Id) == null)
            {
                var transactionPagoSeller = new TransactionPago
                {
                    Id_va = vaSeller!.Id,
                    Id_user = userSeller!.Id,
                    Id_ord = op!.Id,
                    Tipo = "2",
                    Monto = 1450.45
                };
                await context.TransactionsPago.AddAsync(transactionPagoSeller);
                await context.SaveChangesAsync();
            }
            else
            {
                Debug.WriteLine("La transacción ya existe");
            }


            // Exchange Spot
            if (await context.ExchangeSpots.FirstOrDefaultAsync(es => es.RUC == "20227846639") == null)
            {
                var exchangeSpot = new ExchangeSpot
                {
                    RazSocial = "Bodega Juan EIRL",
                    RUC = "20227846639",
                    Pais = "Perú",
                    Ciudad = "Lima",
                    Distrito = "San Juan de Lurigancho",
                    ESFEE = 1.0344
                };
                await context.ExchangeSpots.AddAsync(exchangeSpot);
                await context.SaveChangesAsync();
            }
            else
            {
                Debug.WriteLine("El punto de canje ya existe");
            }

            // System Manager
            if (await context.SystemManagers.FirstOrDefaultAsync(sm => sm.DomicilioFiscal == "Av Próceres 1234 ref Estacion Piramide del Sol") == null)
            {
                var systemManager = new SystemManager
                {
                    Pais = "Perú",
                    Ciudad = "Lima",
                    Distrito = "San Juan de Lurigancho",
                    DomicilioFiscal = "Av Próceres 1234 ref Estacion Piramide del Sol",
                    SMFEE = 1.208
                };
                await context.SystemManagers.AddAsync(systemManager);
                await context.SaveChangesAsync();
            }
            else
            {
                Debug.WriteLine("El administrador del sistema ya existe");
            }


            // Recarga
            var es = await context.ExchangeSpots.FirstOrDefaultAsync(es => es.RUC == "20227846639");
            var sm = await context.SystemManagers.FirstOrDefaultAsync(sm => sm.DomicilioFiscal == "Av Próceres 1234 ref Estacion Piramide del Sol");

            if (await context.Recargas.FirstOrDefaultAsync(re => re.Id_es == es!.Id && re.Id_sm == sm!.Id) == null)
            {
                double montoRecarga = 100.00;
                double GES = es!.ESFEE / 100 * montoRecarga;
                double GSM = sm!.SMFEE / 100 * montoRecarga;
                double MESRA = montoRecarga - GES;
                double MSMRA = montoRecarga - GES - GSM;

                var recarga = new Recarga
                {
                    Monto = montoRecarga,
                    Ges = GES,
                    Gsm = GSM,
                    Mesra = MESRA,
                    Msmra = MSMRA,
                    Id_es = es!.Id,
                    Id_sm = sm!.Id
                };
                await context.Recargas.AddAsync(recarga);
                await context.SaveChangesAsync();
            }
            else
            {
                Debug.WriteLine("La recarga ya existe");
            }

            // Transaction Recarga
            var re = await context.Recargas.FirstOrDefaultAsync(re => re.Id_es == es!.Id && re.Id_sm == sm!.Id);

            if (await context.TransactionsRecarga.FirstOrDefaultAsync(tr => tr.Id_user == userBuyer!.Id && tr.Id_rec == re!.Id) == null)
            {
                var transactionRecarga = new TransactionRecarga
                {
                    Id_va = vaBuyer!.Id,
                    Id_user = userBuyer!.Id,
                    Id_rec = re!.Id,
                    Monto = 100.00
                };
                await context.TransactionsRecarga.AddAsync(transactionRecarga);
                await context.SaveChangesAsync();
            }
            else
            {
                Debug.WriteLine("La transacción ya existe");
            }



            // Solicitud Retiro
            if (await context.SolicitudesRetiro.FirstOrDefaultAsync(sr => sr.Id_s == userSeller!.Id) == null)
            {
                var solicitudRetiro = new SolicitudRetiro
                {
                    Monto = 1200,
                    Estado = "O",
                    Id_s = userSeller!.Id
                };
                await context.SolicitudesRetiro.AddAsync(solicitudRetiro);
                await context.SaveChangesAsync();
            }
            else
            {
                Debug.WriteLine("La solicitud de retiro ya existe");
            }


            // Transaction Retiro
            var sr = await context.SolicitudesRetiro.FirstOrDefaultAsync(sr => sr.Id_s == userSeller!.Id);

            if (await context.TransactionsRetiro.FirstOrDefaultAsync(tr => tr.Id_user == userSeller!.Id && tr.Id_retiro == sr!.Id) == null)
            {
                var transactionRetiro = new TransactionRetiro
                {
                    Id_va = vaSeller!.Id,
                    Id_user = userSeller!.Id,
                    Id_retiro = sr!.Id,
                    Monto = 1200
                };
                await context.TransactionsRetiro.AddAsync(transactionRetiro);
                await context.SaveChangesAsync();
            }
            else
            {
                Debug.WriteLine("La transacción ya existe");
            }
        }


        private static void MostrarErrores(IdentityResult result)
        {
            if (result.Errors != null)
            {
                foreach (var error in result.Errors)
                {
                    Debug.WriteLine(error.Description);
                }
            }
            else
            {
                Debug.WriteLine(result.ToString());
            }
        }
    }
}
