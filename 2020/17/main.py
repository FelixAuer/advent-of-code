import numpy as np

# null bock heute lol

INPUT_FILE = "input.txt"


def main():
    start_config = load_input()
    grid = np.full((25, 25, 25, 25), '.')

    for i in range(len(start_config)):
        for j in range(len(start_config[0])):
            grid[10][i + 10][j + 10][10] = start_config[i][j]

    simulate(grid)


def simulate(grid):
    gen = 0

    while gen < 6:
        next_generation = np.full((25, 25, 25, 25), '.')

        for i in range(1, len(grid) - 1):
            for j in range(1, len(grid[i]) - 1):
                for k in range(1, len(grid[i][j]) - 1):
                    for l in range(1, len(grid[i][j][k]) - 1):

                        count_occupied = check_occupied(grid, i, j, k, l)

                        if grid[i][j][k][l] == '#':
                            if count_occupied in [2, 3]:
                                next_generation[i][j][k][l] = '#'
                            else:
                                next_generation[i][j][k][l] = '.'

                        if grid[i][j][k][l] == '.':
                            if count_occupied == 3:
                                next_generation[i][j][k][l] = '#'
                            else:
                                next_generation[i][j][k][l] = '.'

        grid = next_generation

        gen = gen + 1

    count = 0

    for i in range(1, len(grid) - 1):
        for j in range(1, len(grid[i]) - 1):
            for k in range(1, len(grid[i][j]) - 1):
                for l in range(1, len(grid[i][j][k]) - 1):
                    if grid[i][j][k][l] == '#':
                        count += 1

    print(count)


def check_occupied(grid, i, j, k, l):
    count_occupied = 0
    for di in range(-1, 2):
        for dj in range(-1, 2):
            for dk in range(-1, 2):
                for dl in range(-1, 2):
                    if di == 0 and dj == 0 and dk == 0 and dl == 0:
                        continue
                    if grid[i + di][j + dj][k + dk][l + dl] == '#':
                        count_occupied = count_occupied + 1
    return count_occupied



def load_input():
    return list(map(list, open(INPUT_FILE, "r").read().strip().split("\n")))


if __name__ == '__main__':
    main()
