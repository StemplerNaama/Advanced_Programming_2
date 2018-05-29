#include <iostream>
#include "TwoDim.h"
#include "Bfs.h"
#include "InputProcessing.h"
#include "Tcp.h"
//#include <gtest/gtest.h>

using namespace std;

int main(int argc, char* argv[]) {
    //server commands
    Socket* tcp = new Tcp(1, atoi(argv[1]));
    tcp->initialize();
    /*get the input of grid and obstacles, if it returns NULL (means the input was invalid)
     * so scan it again till we get a valid grid and obstacles
     */
    Grid *map = createGridAndObstacles();
    while (NULL == map)
        map = createGridAndObstacles();
    Bfs currentBfs(map);
    //initialize taxi center
    TaxiCenter* station = new TaxiCenter(map, &currentBfs);
    //running menu
    while (1) {
        menu(station, tcp);
    }
}