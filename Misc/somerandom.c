#include<stdio.h>

// Largest Sum contiguous array:
int maxfun(int x, int y)
{
        return (x>y)?x:y;
}

int sum(int array[], int len)
{
        int max = array[0], maxsofar = 0;
        int count = 0;
        while(count < len)
        {
                maxsofar = maxfun(array[count], maxsofar+array[count]);
                max = (max < maxsofar)?maxsofar:max;
                count++;
        }
        return max;
}
int main()
{
        // int a[] = {-2, -3, -4, -1, -2, -1, -5, -3};
        // int a[] = {-2, -3, 4, -1, -2, 1, 5, -3};
        int a[] =  {4, -2, -3, 4, -1, -2, 1, 5, -3};
        int len = sizeof(a)/sizeof(int), i =0;
        for(;i<len;i++)
                printf(" %d ",a[i]);
        printf("\nSum : %d\n", sum(a, len));
        return 0;
}
