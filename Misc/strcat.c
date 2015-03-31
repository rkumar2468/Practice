/*
	String Operations
*/
#include <stdio.h>

int strlen1(char *s)
{
	int count = 0;
	while(*s != '\0')
	{
		count++;
		s++;
	}
	return count;
}

char *strcat1(char *s1, char *s2)
{
	int len = strlen1(s1) + strlen2(s2);
	char *temp = malloc(len);
	char *res = temp;
	while(*s1 != '\0')
	{	
		*temp = *s1;
		temp++;
		s1++;
	}

	while(*s2 != '\0')
	{
		*temp = *s2;
		s2++;
		temp++;
	}
	*temp='\0';
	return res;
}

int main()
{
	char *s1 = "Hi,", *s2 = "Raj";
	char *s3 = strcat1(s1, s2);
	printf("Concat of %s and %s is %s\n", s1, s2, s3);
	return 0;
}
