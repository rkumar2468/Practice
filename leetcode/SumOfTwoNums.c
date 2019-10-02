/**
 * Definition for singly-linked list.
 * struct ListNode {
 *     int val;
 *     struct ListNode *next;
 * };
 */

typedef struct ListNode List;
List* CreateListNode(int val);
struct ListNode* addTwoNumbers(struct ListNode* l1, struct ListNode* l2){
    if (l1 == NULL) return l2;
    if (l2 == NULL) return l1;
    
    List* result = NULL;
    List* HEAD = NULL;
    int sum = 0, carry = 0, firstElement = 1;
    while (l1 != NULL || l2 != NULL)
    {
        int l1Val = 0, l2Val = 0;
        if (l1 != NULL) l1Val = l1->val;
        if (l2 != NULL) l2Val = l2->val;
        
        sum = l1Val + l2Val + carry;
        carry = sum / 10;
        int value = sum % 10;
        if (firstElement == 1)
        {
            result = CreateListNode(value);
            HEAD = result;
            firstElement = 0;
        }
        else
        {
            // printf("%d \t", value);
            result->next = CreateListNode(value);
            result = result->next;
        }
        
        if(l1 != NULL) l1 = l1->next;
        if(l2 != NULL) l2 = l2->next;
    }
    
    if(carry > 0) result->next = CreateListNode(carry);
    
    return HEAD;
}

List* CreateListNode(int val)
{
    List* res = (List *)malloc(sizeof(List));
    res->val = val;
    res->next = NULL;
    
    return res;
}

