import numpy as np

INPUT_FILE = "input.txt"


class Ship1:
    def __init__(self):
        self.pos_x = 0
        self.pos_y = 0
        self.angle = 0

    def move(self, direction, distance):
        if direction == "N":
            self.pos_y = self.pos_y + distance
            return
        if direction == "S":
            self.pos_y = self.pos_y - distance
            return
        if direction == "W":
            self.pos_x = self.pos_x - distance
            return
        if direction == "E":
            self.pos_x = self.pos_x + distance
            return
        if direction == "F":
            dx, dy = self.direction2cart(distance)
            self.pos_x = self.pos_x + dx
            self.pos_y = self.pos_y + dy
            return
        if direction == "L":
            self.angle = self.angle + distance
            return
        if direction == "R":
            self.angle = self.angle - distance
            return

    def direction2cart(self, distance):
        return round(distance * np.cos(np.deg2rad(self.angle))), round(distance * np.sin(np.deg2rad(self.angle)))


class Waypoint:
    def __init__(self, pos_x, pos_y):
        self.pos_x = pos_x
        self.pos_y = pos_y

    def move(self, direction, distance):
        if direction == "N":
            self.pos_y = self.pos_y + distance
            return
        if direction == "S":
            self.pos_y = self.pos_y - distance
            return
        if direction == "W":
            self.pos_x = self.pos_x - distance
            return
        if direction == "E":
            self.pos_x = self.pos_x + distance
            return
        if direction == "L":
            self.rotate(distance)
            return
        if direction == "R":
            self.rotate(-1 * distance)
            return

    def rotate(self, angle):
        old_x = self.pos_x
        old_y = self.pos_y
        self.pos_x = round(np.cos(np.deg2rad(angle)) * old_x - np.sin(np.deg2rad(angle)) * old_y)
        self.pos_y = round(np.sin(np.deg2rad(angle)) * old_x + np.cos(np.deg2rad(angle)) * old_y)


class Ship2:
    def __init__(self):
        self.pos_x = 0
        self.pos_y = 0
        self.waypoint = Waypoint(10, 1)

    def move(self, direction, distance):
        if direction == "F":
            self.pos_x = self.pos_x + distance * self.waypoint.pos_x
            self.pos_y = self.pos_y + distance * self.waypoint.pos_y
            return
        self.waypoint.move(direction, distance)


def main():
    instructions = load_input()
    part_one(instructions)
    part_two(instructions)


def part_one(instructions):
    ship = Ship1()
    for instruction in instructions:
        ship.move(instruction[0], int(instruction[1:]))
    print(abs(ship.pos_x) + abs(ship.pos_y))


def part_two(instructions):
    ship = Ship2()
    for instruction in instructions:
        ship.move(instruction[0], int(instruction[1:]))
    print(abs(ship.pos_x) + abs(ship.pos_y))


def load_input():
    return open(INPUT_FILE, "r").read().strip().split("\n")


if __name__ == '__main__':
    main()
