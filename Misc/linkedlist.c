#include "linkedlist.h"

void printLL(Node *head)
{
	if (head == NULL )
		return;
	printf(" Linked List: ");
	while(head != NULL){
		printf(" %d ",head->data);
		head = head->next;
	}
	printf("\n");
}


Node *newLL(int data)
{
	Node *temp = (Node *)malloc(sizeof(Node));
	temp->data = data;
	temp->next = NULL;
	return temp;
}

void removell(Node **head, int data)
{
	if (head == NULL ) {
		printf(" Error in removal of %d \n",data);
		return;
	} else {
		Node *temp = *head;
		Node *prev = NULL;
		while(temp != NULL)
		{
			if(temp->data == data) break;
			prev = temp;
			temp = temp->next;
		}
		if(prev != NULL)
			prev->next = temp->next;
		free(temp);
	}
}

void insert(Node **head, int data)
{
	Node *temp = newLL(data);
	temp->next = *head;
	*head = temp;
}

void reverse(Node **head)
{
	if ( *head == NULL ) {
		printf(" Error in reversing the linked list.!\n");
		return;
	}
	else {
		Node *temp = *head;
		Node *prev = NULL;
		Node *first = temp->next;
		while(temp->next != NULL)
		{
			temp->next = prev;
			prev = temp;
			temp = first;
			first = temp->next;
		}
		temp->next = prev;
		*head = temp;		
	}
}

void insertSorted(Node **root, int data)
{
	Node *temp = newLL(data);
	if(*root == NULL)
		*root = temp;
	else {
		Node *tmp = (*root)->next;
		Node *prev = *root;
		while(tmp != NULL)
		{
			if(tmp->data > data && prev->data < data){
				// temp->next = tmp;
				// prev->next = temp;
				break;
			}
			tmp = tmp->next;
			prev = prev->next;
		}
		if ( prev->data < data) {
			temp->next = tmp;
			prev->next = temp;
		} else {
			temp->next = prev;
			*root = temp;
		}
	}
}

void addNumToLL(Node **root, int data, int add)
{
	if (*root == NULL)
		printf("No addition performed.!\n");
	else {
		if (add > 0){
			Node *temp = *root;
			Node *prev = NULL;
			int sum =  0;
			while(temp != NULL && temp->data != data) {
				prev = temp;
				temp = temp->next;
			}
			// if(temp->data != data) return;
			if(temp == NULL) return;
			temp->data += add;	
			sum = temp->data;
			
			if (temp->next != NULL && temp->next->data > temp->data)
				return;
			Node *tmp = temp;
			if(prev != NULL)
				prev->next = temp->next;
			else
				*root = temp->next;		
			while(temp != NULL)
			{
				if (sum < temp->data)
					break;
				prev = temp;
				temp = temp->next;
			}
			tmp->next = temp;
			if (prev != NULL)
				prev->next = tmp;
			
		}
	}
}

void facebook(Node **head)
{
	Node *ll = *head;
	if (ll == NULL) {
		printf (" Error.!\n");
		return;
	}
	Node *prev = NULL;
	Node *tmp1 = NULL, *tmp2 = NULL, *temp = NULL;
	while( ll != NULL )
	{
		prev = ll;
		temp = ll;
		ll = ll->next;
		if (ll == NULL || ll->next == NULL)
		{
			// printf(" Error.!\n);
			break;
		} else {
			while(temp->next->next != NULL)
			{
				tmp1 = temp;
				temp = temp->next;
				tmp2 = temp->next;
			}
			if (tmp1 != NULL && tmp2 != NULL && temp != NULL)
			{
				prev->next = temp;
				tmp2->next = ll;
				tmp1->next = NULL;
			}
		}	 
	}
}

void facebookRec(Node **head)
{
	Node *ll = *head;
		if (ll == NULL) {
		printf (" Error.!\n");
		return;
	}
	Node *prev = NULL;
	Node *tmp1 = NULL, *tmp2 = NULL, *temp = NULL;
	prev = ll;
	temp = ll;
	ll = ll->next;
	if (ll == NULL || ll->next == NULL)
	{
		// printf(" Error.!\n);
		return;
	} else {
		while(temp->next->next != NULL)
		{
			tmp1 = temp;
			temp = temp->next;
			tmp2 = temp->next;
		}
		if (tmp1 != NULL && tmp2 != NULL && temp != NULL)
		{
			prev->next = temp;
			tmp2->next = ll;
			tmp1->next = NULL;
		}
	}
	facebookRec(&ll);
}
void reverseUpdt(Node **head)
{
	Node *temp = *head;
	Node *prev = NULL;
	Node *nex = NULL;
	if (temp == NULL)
		return;
	while(temp != NULL)
	{
		nex = temp->next;
		temp->next = prev;
		prev = temp;		
		temp = nex;		
	}
	if (prev != NULL )
		*head = prev;
}
// Given a linked list and a window //
// reverse the windowed elements of that list //
// For Eg: 1 -> 2 -> 3 -> 4 -> 5 -> 6 -> 7 -> 8 -> 9 -> NULL //
// Say window = 3 //
// Solution: 3 -> 2 -> 1 -> 6 -> 5 -> 4 -> 9 -> 8 -> 7 -> NULL //

void reverseLLW(Node **head, int window, Node *parent)
{
	if(*head == NULL) return;
	Node *temp = (*head)->next;
	Node *prev = *head;
	Node *tmp = NULL;
	int count = 0;
	if (temp == NULL) return;
	while(count < window-1 && temp != NULL)
	{
		tmp = temp->next;
		temp->next = prev;
		prev = temp;
		temp = tmp;
		count++;
	}
	(*head)->next = tmp;
	temp = *head;
	*head = prev;
	if (parent != NULL)
		parent->next = *head;
	if (tmp == NULL) return;
	reverseLLW(&tmp, window, temp);
}
