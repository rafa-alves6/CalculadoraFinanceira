using System;

public class SacTable
{
	public int InstallmentNumber { get; set; }
	public double OutstandingPrincipal { get; set; }
	public double Amortization { get; set; }
	public double InterestValue { get; set; }
	public double InstallmentValue { get; set; }
	public double TotalPaid { get; set; }

	public SacTable(int installmentNumber, double outstandingPrincipal, double amortization, double interestValue, double installmentValue, double totalPaid)
	{
		InstallmentNumber = installmentNumber;
		OutstandingPrincipal = outstandingPrincipal;
		Amortization = amortization;
		InterestValue = interestValue;
		InstallmentValue = installmentValue;
		TotalPaid = totalPaid;
	}
}
