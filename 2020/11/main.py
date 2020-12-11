import numpy as np
import copy
import time

INPUT_FILE = "input.txt"


def main():
    seat_plan = load_input()
    prepare_seat_plan(seat_plan)

    print(part_one(seat_plan))
    print(part_two(seat_plan))


def part_one(seat_plan):
    return simulate(seat_plan, check_occupied_1, 4)


def part_two(seat_plan):
    return simulate(seat_plan, check_occupied_2, 5)


def simulate(seat_plan, check_occupied_function, tolerance):
    gen = 0

    while True:
        seats_changed = False
        next_generation = copy.deepcopy(seat_plan)

        for y in range(1, len(seat_plan) - 1):
            for x in range(1, len(seat_plan[y]) - 1):
                if seat_plan[y][x] == '.':
                    next_generation[y][x] = '.'
                    continue

                count_occupied = check_occupied_function(seat_plan, x, y)

                if seat_plan[y][x] == '#' and count_occupied >= tolerance:
                    next_generation[y][x] = 'L'
                    seats_changed = True
                    continue

                if seat_plan[y][x] == 'L' and count_occupied == 0:
                    next_generation[y][x] = '#'
                    seats_changed = True
                    continue

        seat_plan = next_generation

        # if gen % 2 == 0:
        #    print_rows = [''.join(row) for row in seat_plan]
        #    print("\n".join(print_rows))
        #    print("\n\n")
        #    time.sleep(0.5)

        gen = gen + 1

        if not seats_changed:
            return sum([row.count('#') for row in seat_plan])


def check_occupied_1(seat_plan, x, y):
    count_occupied = 0
    for dx in range(-1, 2):
        for dy in range(-1, 2):
            if dx == 0 and dy == 0:
                continue
            if seat_plan[y + dy][x + dx] == '#':
                count_occupied = count_occupied + 1
    return count_occupied


def check_occupied_2(seat_plan, x, y):
    count_occupied = 0
    for dx in range(-1, 2):
        for dy in range(-1, 2):
            if dx == 0 and dy == 0:
                continue

            multiplier = 1
            while True:
                ny = y + multiplier * dy
                nx = x + multiplier * dx
                if nx <= 0 or ny <= 0 or ny >= len(seat_plan) or nx >= len(seat_plan[0]):
                    break

                if seat_plan[ny][nx] == '#':
                    count_occupied = count_occupied + 1
                    break

                if seat_plan[ny][nx] == 'L':
                    break

                multiplier = multiplier + 1
    return count_occupied


def prepare_seat_plan(seat_plan):
    for row in seat_plan:
        row.insert(0, '.')
        row.append('.')
    seat_plan.insert(0, list(np.repeat('.', len(seat_plan[0]))))
    seat_plan.append(list(np.repeat('.', len(seat_plan[0]))))


def load_input():
    return list(map(list, open(INPUT_FILE, "r").read().strip().split("\n")))


if __name__ == '__main__':
    main()
