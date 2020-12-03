INPUT_FILE = "input.txt"
from math import prod


def main():
    lines = load_input()

    # part 1
    print(run_toboggan(lines, 3, 1))

    # part 2
    print(prod([run_toboggan(lines, dx, dy) for [dx, dy] in [[1, 1], [3, 1], [5, 1], [7, 1], [1, 2]]]))


def run_toboggan(lines, d_right, d_down):
    trees = 0
    width = len(lines[0])

    position_x = 0
    position_y = 0

    while position_y < len(lines):
        if lines[position_y][position_x] == '#':
            trees = trees + 1

        position_x = (position_x + d_right) % width
        position_y = position_y + d_down

    return trees


def load_input():
    return open(INPUT_FILE, "r").read().strip().split("\n")


if __name__ == '__main__':
    main()
