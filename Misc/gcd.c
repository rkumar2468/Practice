/*
	Greatest Common Divisor
*/
#include <stdio.h>

int gcd(int a, int b)
{
	if(a<0 || b<0) return -1;
	if(a==0) return b;
	else if(b==0) return a;
	if(a>b)
		return gcd(a-b,b);
	else 
		return gcd(a,b-a);
	
}

int main()
{
	int m, n;
	int ret;
	printf("Enter two positive integers: ");
	scanf("%d %d",&m,&n);
	ret = gcd(m,n);
	if(gcd>0)
		printf("Gcd of %d and %d : %d\n", m, n, ret);
	
	return 0;
}
