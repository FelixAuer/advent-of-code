import string

INPUT_FILE = "input.txt"


def main():
    forms = load_input()
    print(sum(map(count_anyone, forms)))
    print(sum(map(count_everyone, forms)))


def count_anyone(form):
    return len(set(''.join(form.split())))


def count_everyone(form):
    group_size = len(form.split())
    return len([answer for answer in string.ascii_lowercase if form.count(answer) == group_size])


def load_input():
    return open(INPUT_FILE, "r").read().strip().split("\n\n")


if __name__ == '__main__':
    main()
