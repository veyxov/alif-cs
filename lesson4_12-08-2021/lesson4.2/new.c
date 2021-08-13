#include <stdio.h>
#include <math.h>

#define min(a, b) (((a) < (b)) ? (a) : (b))
int main(){

    int input, row, column;

    printf("input a number: ");
    scanf("%d", &input);
    printf("");

    input = 2*(input-1) + 1;
    int pivot = input;
    int total_spaces = input*2+1;
    for(row = 0; row < total_spaces; row++){
        for(column = 0; column < total_spaces; column ++){
            if(min(abs(total_spaces - row -1),abs(0 - row)) +
                  min(abs(total_spaces - column -1),abs(0 - column)) 
                        >= input && (row%2 != column %2)){
                printf("*");
            }
            else printf(" ");
       }
       printf("\n");
    } 
}

