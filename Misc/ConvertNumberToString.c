#include <stdio.h>
int stack[10];
static int pos = 0;
int check = 0;

/*
	Stack operations.
*/
void push(int a)
{
	if(pos == 10) printf("Overflow.\n");
	stack[pos] = a;
	pos++;
}

int pop()
{
	if(pos == 0) return -1;
	return stack[--pos];
}


void reverse(char *str, int len)
{
	char ch, ch1;
	int i = 0;
	while(i < len/2)
	{
		ch = str[i];
		ch1 = str[len-i-1];
		str[i] = ch1;
		str[len-i-1] = ch;
		i++;
	}
}

void printTens(int ch) {
	if(ch == 1) 
		check=1;
	else if(ch != 0) {
		switch (ch)
		{
			// case 1:	printf(" one "); break;
			case 2:	printf(" twenty "); break;
			case 3:	printf(" thirty "); break;
			case 4:	printf(" forty "); break;
			case 5:	printf(" fifty "); break;
			case 6:	printf(" sixty "); break;
			case 7:	printf(" seventy "); break;
			case 8:	printf(" eighty "); break;
			case 9:	printf(" ninety "); break;
			default:printf(" "); break;
		}
	}
	ch = 0;
}
void printbyCount(int ch){
	switch (ch)
	{
		// case 1:	printf(" "); break; // Individual numbers.
		case 2:	printTens(ch); break;
		case 3:	printf(" hundred "); break;
		case 4:	printf(" thousand "); break;
		case 5:	printTens(ch); break;
		case 6:	printf(" million "); break;
		default:printf(" "); break;
	}
}

void printNum(int ch)
{
	switch (ch)
	{
		case 1:	if(check != 1) printf(" one "); else printf(" eleven "); break;
		case 2:	if(check != 1) printf(" two "); else printf(" twelve "); break;
		case 3:	if(check != 1) printf(" three "); else printf(" thirteen "); break;
		case 4:	if(check != 1) printf(" four "); else printf(" fourteen "); break;
		case 5:	if(check != 1) printf(" five "); else printf(" fifteen "); break;
		case 6:	if(check != 1) printf(" six "); else printf(" sixteen "); break;
		case 7:	if(check != 1) printf(" seven "); else printf(" seventeen "); break;
		case 8:	if(check != 1) printf(" eight "); else printf(" eighteen "); break;
		case 9:	if(check != 1) printf(" nine "); else printf(" nineteen "); break;
		default:printf(" "); break;
	}
}

void printString(int ch)
{
	int x = ch;
	int count=0;
	while(x > 0) {
		push(x%10);
		x = x/10;
		count++;
	}
	while((x = pop()) != -1) {
		if(x != 0) {
			if(count != 2)
				printNum(x);
			printbyCount(count--);
		} else count--;
	}
	
}

void usage(){
	printf("Usage: ConvertNumberToString <Number>\n");
}

int main(int argc, char **argv){
/*	int i=0, j=0;
	i++;
	++j;
	printf("I: %d, J: %d\n",i,j);
	char str[] = "hello";
	printf("%s\n", str);
	reverse(str, 5);
	printf("%s\n", str);	*/
	int ch;
	// printf("Enter a number: ");
	// scanf("%d",&ch);
	if(argv[1] == NULL) {
		printf("Error in usage.!\n\n");		
		usage();
		return -1;
	}
	ch = atoi(argv[1]);	
	printString(ch);
	printf("\n");
	return 0;
}
