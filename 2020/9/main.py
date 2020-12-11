import itertools

INPUT_FILE = "input.txt"
CYPHER_LENGTH = 25


def main():
    cypher = load_input()

    index = 0
    invalid_number = 0

    for i in range(CYPHER_LENGTH, len(cypher)):
        combs = [x + y for [x, y] in itertools.combinations(cypher[i - CYPHER_LENGTH: i], 2)]
        if cypher[i] not in combs:
            invalid_number = cypher[i]
            index = i
            break

    print(invalid_number)

    for i in range(0, index - 1):
        for j in range(1, CYPHER_LENGTH):
            cypher_slice = cypher[i:i + j]
            sum_slice = sum(cypher_slice)

            if sum_slice == invalid_number:
                print(min(cypher_slice) + max(cypher_slice))
                return

            if sum_slice > invalid_number:
                break


def load_input():
    return list(map(int, open(INPUT_FILE, "r").read().strip().split("\n")))


if __name__ == '__main__':
    main()
