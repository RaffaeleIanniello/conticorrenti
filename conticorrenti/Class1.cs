using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conticorrenti
{
    public class ContiCorrenti
    {
        public string NomeCorrentista { get; set; }
        public string CognomeCorentista { get; set; }
        public DateTime DataDiApertura = DateTime.Now;
        public int NumeroDiConto { get; set; }
        public double SaldoConto { get; set; }
        public string MovimentoSaldo { get; set; }
        public double cifra { get; set; }
        public static int numeroContoC = 4;
        public ContiCorrenti() { }
        public ContiCorrenti(string nomeC, string cognomeC, DateTime dataApertura, int numeroC, double saldo)
        {
            NomeCorrentista = nomeC;
            CognomeCorentista = cognomeC;
            DataDiApertura = dataApertura;
            NumeroDiConto = numeroC;
            SaldoConto = saldo;
        }
        public ContiCorrenti(int numeroConto, string movimento, double cifra)
        {
            NumeroDiConto = numeroConto;
            MovimentoSaldo = movimento;
            this.cifra = cifra;
        }
        public static List<ContiCorrenti> contiCorrenti = new List<ContiCorrenti>();
        public static List<ContiCorrenti> movimenti = new List<ContiCorrenti>();
        public static void menuStart()
        {
            contiCorrenti.Add(new ContiCorrenti("Mario", "Rossi", DateTime.Now, 1, 0));
            contiCorrenti.Add(new ContiCorrenti("Irene", "Bianchi", DateTime.Now, 2, 0));
            contiCorrenti.Add(new ContiCorrenti("Giovanni", "Vasaio", DateTime.Now, 3, 0));
            contiCorrenti.Add(new ContiCorrenti("Silvio", "Berlusconi", DateTime.Now, 4, 0));


            while (true)
            {
                Console.WriteLine("===== Benvenuto nella gestione conti correnti.");
                Console.WriteLine("1) Apri Conto corrente.");
                Console.WriteLine("2) Effettua una operazione");
                Console.WriteLine("3) Stampa movimenti del cc.");
                Console.WriteLine("4) Stampa il saldo di tutti i cc.");
                Console.WriteLine("5) Esci");
                Console.Write("Digita un numero:");
                int scelta = int.Parse(Console.ReadLine());
                if (scelta == 1)
                {
                    nuovoConto();
                }
                else if (scelta == 2)
                {
                    saldoConto();
                }
                else if (scelta == 3)
                {
                    stampaMovimentiConto();
                }
                else if (scelta == 4)
                {
                    stampaSaldi();
                }
                else if (scelta == 5)
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Scelta non valida");
                }
            }


        }
        public static void nuovoConto()
        {
            numeroContoC++;
            DateTime dateTime = DateTime.Now;
            Console.Write("Inserisci nome :");
            string nome = Console.ReadLine();
            Console.Write("Inserisci cognome : ");
            string cognome = Console.ReadLine();
            contiCorrenti.Add(new ContiCorrenti(nome, cognome, dateTime, numeroContoC, 0));

            Console.WriteLine("Nuovo Conto aperto correttamente");

        }

        public static void saldoConto()
        {
            Console.WriteLine("Digita il numero del conto corrente");
            int numeroConto = int.Parse(Console.ReadLine());
            Console.WriteLine("Che operazione vuoi fare?(versamento/prelievo)");
            string scelta = Console.ReadLine();
            if (scelta == "versamento")
            {
                Console.WriteLine("Quanto vuoi versare?");
                double versamento = double.Parse(Console.ReadLine());
                foreach (ContiCorrenti conti in contiCorrenti)
                {
                    if (conti.NumeroDiConto == numeroConto)
                    {
                        conti.SaldoConto += versamento;
                        movimenti.Add(new ContiCorrenti(conti.NumeroDiConto, "versamento", versamento));
                        Console.WriteLine("Versamento effettuato correttamente.");
                    }
                }
            }
            else if (scelta == "prelievo")
            {
                Console.WriteLine("Quanto vuoi prelevare?");
                double prelievo = double.Parse(Console.ReadLine());
                foreach (ContiCorrenti conti in contiCorrenti)
                {
                    if (conti.NumeroDiConto == numeroConto)
                    {
                        if (conti.SaldoConto >= prelievo)
                        {
                            conti.SaldoConto -= prelievo;
                            movimenti.Add(new ContiCorrenti(conti.NumeroDiConto, "prelievo", prelievo));
                            Console.WriteLine("Prelievo effettuato correttamente");
                        }
                        else
                        {
                            Console.WriteLine("Il saldo attuale e' inferiore alla richiesta di prelievo.");
                        }
                    }

                }
            }
            else
            {
                Console.WriteLine("Scelta non valida.");
            }
        }
        public static void stampaMovimentiConto()
        {
            Console.Write("Digita il numero del conto che vuoi cercare :");
            int nConto = int.Parse(Console.ReadLine());
            foreach (ContiCorrenti conto in movimenti)
            {
                if (conto.NumeroDiConto == nConto)
                {
                    Console.WriteLine($"Il conto corrente numero :{conto.NumeroDiConto} ha effettuato un {conto.MovimentoSaldo} di euro {conto.cifra}");
                }
                else
                {
                    Console.WriteLine("Numero di conto non valido.");
                }
            }

        }
        public static void stampaSaldi()
        {
            foreach (ContiCorrenti conto in contiCorrenti)
            {
                Console.WriteLine($"Il conto corrente numero :{conto.NumeroDiConto} possiede un saldo di: {conto.SaldoConto}");
            }
        }


    }
}


/* --1) Numero totale degli ordini
SELECT COUNT(*) AS NumeroTotaleOrdini
FROM Orders;

--2) Numero totale di clienti
SELECT COUNT(*) AS NumeroTotaleClienti
FROM Customers;

--3) Numero totale di clienti che abitano a Londra
SELECT COUNT(*) AS NumeroClientiLondra
FROM Customers
WHERE City = 'London';

--4) La media aritmetica del costo del trasporto di tutti gli ordini effettuati
SELECT AVG(Freight) AS MediaCostoTrasporto
FROM Orders;

--5) La media aritmetica del costo del trasporto dei soli ordini effettuati dal cliente “BOTTM”
SELECT AVG(Freight) AS MediaCostoTrasportoClienteBOTTM
FROM Orders
WHERE CustomerID = 'BOTTM';

--6) Totale delle spese di trasporto effettuate raggruppate per id cliente
SELECT CustomerID, SUM(Freight) AS TotaleSpeseTrasporto
FROM Orders
GROUP BY CustomerID;

--7) Numero totale di clienti raggruppati per città di appartenenza
SELECT City, COUNT(*) AS NumeroClientiPerCitta
FROM Customers
GROUP BY City;

--8) Totale di UnitPrice * Quantity raggruppato per ogni ordine
SELECT OrderID, SUM(UnitPrice * Quantity) AS TotalePerOrdine
FROM OrderDetails
GROUP BY OrderID;

--9) Totale di UnitPrice * Quantity solo dell’ordine con id=10248
SELECT SUM(UnitPrice * Quantity) AS TotalePerOrdine10248
FROM OrderDetails
WHERE OrderID = 10248;

--10) Numero di prodotti censiti per ogni categoria
SELECT CategoryName, COUNT(*) AS NumeroProdottiPerCategoria
FROM Categories
JOIN Products ON Categories.CategoryID = Products.CategoryID
GROUP BY CategoryName;

--11) Numero totale di ordini raggruppati per paese di spedizione (ShipCountry)
SELECT ShipCountry, COUNT(*) AS NumeroOrdiniPerPaese
FROM Orders
GROUP BY ShipCountry;

--12) La media del costo del trasporto raggruppati per paese di spedizione (ShipCountry)
SELECT ShipCountry, AVG(Freight) AS MediaCostoTrasportoPerPaese
FROM Orders
GROUP BY ShipCountry; */