#include "linkedlist.h"

int main(){
	Node *ll;
	insertSorted(&ll,1);
	insertSorted(&ll,2);
	insertSorted(&ll,3);
	insertSorted(&ll,4);
	insertSorted(&ll,5);
	insertSorted(&ll,6);
	insertSorted(&ll,7);
	insertSorted(&ll,8);
	insertSorted(&ll,9);
	insertSorted(&ll,10);
	printLL(ll);
	// reverse(&ll);
	// addNumToLL(&ll, 1, 19);
	// facebookRec(&ll);
	reverseUpdt(&ll);
	printLL(ll);
	return 0;
}
