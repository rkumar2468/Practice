/*
	Hailstone sequence
*/
#include <stdio.h>

void hailstone()
{
	int ch;
	printf("Enter a non-zero natural number: ");
	scanf("%d",&ch);
	if(ch <= 0) {
		printf("Invalid input.\n");
		return;
	}
	while(ch != 1)
	{
		printf("%d, ",ch);
		if(ch%2 == 1)
		 	ch = 3*ch + 1;
		else 
			ch /= 2;
	}
	printf("1\n");
}

int main()
{
	hailstone();
	return 0;
}
