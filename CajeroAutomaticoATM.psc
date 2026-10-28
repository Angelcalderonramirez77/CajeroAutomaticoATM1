Algoritmo CajeroAutomaticoATM
	Definir nombreCliente, numeroCuenta Como Caracter
	Definir saldoDisponible, depositar ,retirar Como Real 
	Definir opciones Como Entero
	Definir estadoCuenta Como Logico
	
	opciones= 0
	saldoDisponible = 1500
	numeroCuenta= "100245"
	estadoCuenta = Verdadero 	
	depositar = 0
	retirar = 0
	
	Escribir "=============================="
	Escribir "Bienvenido a su ATM favorito "
	Escribir "=============================="
	
	Escribir "Ingrese su nombre del cliente "
	leer nombreCliente
	
	Escribir "Ingrese su numero de cuenta "
	leer numeroCuenta
	
	Mientras numeroCuenta <> "100245"  Hacer
		Escribir "Error: Su numero de cuenta  no es el correcto"
		Escribir "Intente de nuevo"
		Leer numeroCuenta
		
	Fin Mientras
	Escribir "Numero de cuenta es el correcto"

		Mientras estadoCuenta Hacer
		Escribir "==========Menu Operaciones=========="
		Escribir "1.Consultar Saldo "
		Escribir "2.Depositar "
		Escribir "3.Retirar "
		Escribir "4.Salir  "
		Leer opciones 
		
		Segun opciones Hacer
			1:
				Escribir "El saldo dispobible es RD$", saldoDisponible 
			2:
				Escribir "Ingrese el monto a depositar"
				Leer depositar 
				
				saldoDisponible = saldoDisponible + depositar 
			3:
				Escribir "Ingrese el monto a retirar"
				Leer retirar
				Si retirar <= depositar Entonces
					saldoDisponible = saldoDisponible - retirar
					Escribir "Retiro exitoso"
				SiNo
					Escribir "Fondos insuficientes. Su saldo es menor al monto solicitado."
					retirar = 0 
					Esperar Tecla
				Fin Si
				
			4: 
				Escribir "Cerrar seccion"
				estadoCuenta = false 
				
			De Otro Modo:
				Escribir "Error: Opción inválida. Intente de nuevo."
				Escribir "Presione una tecla para volver al menú..."
				Esperar Tecla				
		Fin Segun
		
	Fin Mientras

	Escribir "======================================================="
	Escribir "Gracias por usar nuestro Cajero Sr(a)", nombreCliente
	Escribir "El monto despsitado fue RD$", depositar
	Escribir "El monto Retirado fue RD$", retirar
	Escribir "Su Saldo Actual es de RD$", saldoDisponible
	Escribir "=================================================="

FinAlgoritmo
