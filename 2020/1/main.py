INPUT_FILE = "input.txt"


def main():
    x = load_input()

    print([a * b for a in x for b in x if a + b == 2020][0])
    print([a * b * c for a in x for b in x for c in x if a + b + c == 2020][0])


def load_input():
    return list(map(int, open(INPUT_FILE, "r").read().strip().split("\n")))


if __name__ == '__main__':
    main()
