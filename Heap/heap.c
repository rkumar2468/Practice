/*
	Implementation:	Heap Data structure
	Has: buildHeap, makeHeap, deleteMin and bubbleUp operations
*/

#include <stdio.h>

int length(int *a)
{	
	int count = 0;
	while(*a++ != '\0')
		count++;
	return count;
}

void makeHeap(int *a, int pos, int len)
{
	int left = (2*pos + 1), right = (2*pos + 2);
	int min = (a[left] > a[right])? right:left;
	if(pos >= len || left > len || right > len) return;
	int temp = a[pos];
	if(a[pos] > a[min]){
		a[pos] = a[min];
		a[min] = temp;
		makeHeap(a, min, len);
	}
}

void bubbleUp(int *a, int pos)
{
	int parent = pos/2;	
	if(pos == 0) return;
	if(a[pos] < a[parent])
	{
		int temp = a[parent];
		a[parent] = a[pos];
		a[pos] = temp;
		bubbleUp(a, parent);
	}	
}

void buildHeap(int *a, int len)
{
	int i = 1;
	for(;i<len; i++)
		bubbleUp(a, i);
}

int deleteMin(int *a, int len)
{
	int min = a[0];
	a[0] = a[len-1];
	len--;
	a[len] = '\0';
	makeHeap(a, 0, len);
	return min;
}

void printArray(int *a)
{
	while(*a != '\0')
		printf("%d ",*a++);
	printf("\n");
}

int main(){
	int array[10] = {10,9,8,7,6,5,4,3,2,1};
	int min;
	int len = sizeof(array)/sizeof(int);
	printArray(array);
	buildHeap(array, len);	
	printArray(array);
	min = deleteMin(array, len);
	printArray(array);	
	min = deleteMin(array, len-1);
	printArray(array);	
	return 0;
}
