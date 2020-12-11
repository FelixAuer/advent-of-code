import numpy as np

INPUT_FILE = "input.txt"


def main():
    joltage = load_input()
    joltage.sort()

    joltage.insert(0, 0)
    joltage.append(max(joltage) + 3)

    part_one(joltage)
    [connections, _] = dynamic_step(joltage, {})
    print(connections)


def dynamic_step(joltage, results):
    goal = joltage[-1]
    if goal in results:
        return [results[goal], results]

    if len(joltage) == 1:
        return [1, results]

    adapters = joltage[:-1]

    sum_connections = 0

    for i in range(1, len(adapters) + 1):
        if goal - adapters[-i] <= 3:
            if i == 1:
                [count_sub, results] = dynamic_step(adapters, results)
            else:
                [count_sub, results] = dynamic_step(adapters[:-i + 1], results)

            sum_connections = sum_connections + count_sub

    results[goal] = sum_connections
    return [sum_connections, results]


def part_one(joltage):
    differences = list(np.subtract(joltage[1:], joltage[:-1]))
    print(differences.count(1) * differences.count(3))


def load_input():
    return list(map(int, open(INPUT_FILE, "r").read().strip().split("\n")))


if __name__ == '__main__':
    main()
