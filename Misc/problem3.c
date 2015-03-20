/*
	Find k for which n*(2^k) >= (k!) breaks for k=k+1 given "n"
*/
#include <stdio.h>

int factorial(int n)
{
	if(n==0) return 1;
	while(n>0)
	{
		return n*factorial(n-1);
	}
}

int power(int base, int exp)
{
	if(exp == 0) return 1;
	return base*power(base, exp-1);
}

int computation(int n)
{
	int k = 0;
	if(n < 0) return -1;
	else if(n == 0) return k;
	while( n*(power(2,k)) >= factorial(k))
		k++;
	return (k-1);
	
}

int main()
{
	int n, ret;
	printf("Enter a number: ");
	scanf("%d",&n);
	ret = computation(n);
	if(ret >= 0)
		printf("Value satisfying the expression (n*(2^k) >= (k!)) : %d\n", ret);
	return 0;
}
