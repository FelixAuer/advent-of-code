import math
from matplotlib import pyplot as plt
from celluloid import Camera

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

    empty = [seat for seat in range(max(ids)) if (seat - 1) in ids and (seat + 1) in ids and seat not in ids][0]
    print(empty)

    plot_boarding(boarding_passes, empty)


def plot_boarding(boarding_passes, empty):
    fig = plt.figure()
    ax = plt.axes()
    ax.set_ylim(-1, 10)
    ax.set_xlim(-2, 110)
    camera = Camera(fig)
    plt.pause(15)
    for i in range(len(boarding_passes)):
        draw_filled(ax, boarding_passes, i)
        camera.snap()
    draw_filled(ax, boarding_passes, len(boarding_passes))
    draw_seat(ax, [empty % 8], [math.floor(empty / 8)], 'bo')
    camera.snap()
    animation = camera.animate()
    animation.save('animation.gif', writer='PillowWriter', fps=30)


def draw_filled(ax, boarding_passes, i):
    rows = []
    cols = []
    for j in range(i):
        boarding_pass = boarding_passes[j]
        rows.append(binary_search(0, 127, boarding_pass[:7], "F", "B"))
        cols.append(binary_search(0, 7, boarding_pass[-3:], "L", "R"))
    draw_seat(ax, cols, rows, 'ro')


def draw_seat(ax, cols, rows, style):
    cols = list(map(map_seat, cols))
    ax.plot(rows, cols, style)


def map_seat(col):
    if col in [6, 7]:
        return col + 2
    if col in [2, 3, 4, 5]:
        return col + 1

    return col


def load_input():
    return open(INPUT_FILE, "r").read().strip().split("\n")


if __name__ == '__main__':
    main()
