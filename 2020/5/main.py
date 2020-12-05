import math

INPUT_FILE = "input.txt"


def binary_search(lo, hi, code, lower, higher):
    if lo == hi:
        return lo

    if code[0] == lower:
        return binary_search(lo, math.floor((hi + lo) / 2), code[1:], lower, higher)
    if code[0] == higher:
        return binary_search(math.ceil((hi + lo) / 2), hi, code[1:], lower, higher)


def get_id(boarding_pass):
    return 8 * binary_search(0, 127, boarding_pass[:7], "F", "B") + binary_search(0, 7, boarding_pass[-3:], "L", "R")


def main():
    boarding_passes = load_input()
    ids = list(map(get_id, boarding_passes))

    print(max(ids))

    empty = [seat for seat in range(max(ids)) if (seat - 1) in ids and (seat + 1) in ids and seat not in ids]
    print(empty[0])


def load_input():
    return open(INPUT_FILE, "r").read().strip().split("\n")


if __name__ == '__main__':
    main()
