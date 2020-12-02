INPUT_FILE = "input.txt"


def main():
    input_lines = load_input()
    valid_passwords1 = 0
    valid_passwords2 = 0

    for line in input_lines:
        [times, character, password] = line.split()

        constraints = list(map(int, times.split('-')))
        character = character[0]

        if constraints[0] <= password.count(character) <= constraints[1]:
            valid_passwords1 = valid_passwords1 + 1

        occurrences = [password[i - 1] for i in constraints]
        if occurrences.count(character) == 1:
            valid_passwords2 = valid_passwords2 + 1

    print(valid_passwords1)
    print(valid_passwords2)


def load_input():
    return open(INPUT_FILE, "r").read().strip().split("\n")


if __name__ == '__main__':
    main()
