/* Brazil uses two amortizations systems:
 * PRICE/French amortization system;
 * Constant amortization system (a.k.a. SAC).
 * 
 * Using the PRICE system, a loan's installments are to be paid in an equal amount over time, as per this formula:
 * 
 * PMT = PV * ((1+i)^n * i / (1+i)^n - 1,
 * 
 * PMT being equal to the value of the installments to be paid;
 * PV being equal to the Present Value (or the loan value);
 * i being the interest rate;
 * n being the amount of installments (e.g. 12, if it is a 1-yr loan).
*/

static double CalculatePRICE(Loan loan)
{
    double i = loan.InterestRate / 100;
    int n = loan.Time;
    double pv = loan.PresentValue;
    double factor = Math.Pow(1+i, n);
    double pmt = pv * (i * factor) / (factor - 1);

    return pmt;
}

/*
 * Meanwhile, SAC's installment value goes down with time, whilst having a fixed amortization value.
 * This is because the installment value is defined by the amortization value added to the interest value.
 * To get the fixed amortization value, we divide the present value by the time period (PV/n).
 * 
 * Example: A $1000, 10-month loan with a monthly interest rate of 1% would have an amortization value of 100 (1000/10),
 * and its interest value would be calculated monthly, based on the outstanding principal, which then is
 * gradually lowered until it reaches 0.
*/
static List<SacTable> CalculateSAC(Loan loan)
{
    List <SacTable> tables = [];
    double amortizationValue = loan.PresentValue / loan.Time;
    double outstandingPrincipal = loan.PresentValue;
    double interestRate = loan.InterestRate / 100;
    int installmentCounter = 0;
    double totalPaid = 0;
    
    for(int i = 0; i < loan.Time; i++)
    {
        double interestValue = outstandingPrincipal * interestRate;
        double installmentValue = amortizationValue + interestValue;
        outstandingPrincipal -= amortizationValue;
        installmentCounter++;
        totalPaid += installmentValue;
        SacTable table = new(installmentCounter, outstandingPrincipal, amortizationValue, interestValue, installmentValue, totalPaid);
        tables.Add(table);
    }
    return tables;
}

double presentValue = 100000.3, interestRate = 1.5;
int timeLoan = 120;
Loan l1 = new(presentValue, interestRate, timeLoan, "SAC");
double price = CalculatePRICE(l1);
double totalPaidPrice = price * timeLoan;
Console.WriteLine($"Tabela PRICE: R$ {price:F2}");
Console.WriteLine($"Total pago na tabela price: R$ {totalPaidPrice:F2}");
List<SacTable> sacTable = CalculateSAC(l1);

foreach (SacTable table in sacTable)
{
    Console.Write($"Parcela: {table.InstallmentNumber} | ");
    Console.Write($"Saldo devedor: R$ {table.OutstandingPrincipal:F2} | ");
    Console.Write($"Amortização: R$ {table.Amortization:F2} | ");
    Console.Write($"Juros: R$ {table.InterestValue:F2}  | ");
    Console.Write($"Prestação: R$ {table.InstallmentValue:F2}  | ");
    Console.Write($"Total pago: R$ {table.TotalPaid:F2}");
    Console.WriteLine("\n");
}
