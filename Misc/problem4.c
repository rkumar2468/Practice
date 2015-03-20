/*
	Finding if the number is perfect, deficient or abundant
	Perfect: Sum of factors = Number
	Deficient: Sum of factors < Number
	Abundant: Sum of factors > Number
*/
#include<stdio.h>

int sumoffactors(int n)
{
	int fact = 1;
	int result=0;
	while(fact < n)
	{
		if(n%fact == 0)
			result+=fact;
		fact+=1;
	}
	return result;
}

int main()
{
	int ch, sum;
	printf("Enter a number: ");
	scanf("%d",&ch);
	if(ch <= 0) return 0;
	sum = sumoffactors(ch);
	if(sum == ch)
		printf("Perfect.!\n");
	else if (sum < ch)
		printf("Deficient.!\n");
	else 
		printf("Abundant.!\n");
	return 0;

}
