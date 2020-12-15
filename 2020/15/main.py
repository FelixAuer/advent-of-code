def main():
    run(2020)
    run(30000000)


def run(limit):
    numbers = [0, 14, 6, 20, 1, 4]
    turn = 1
    last_turns = {}

    for number in numbers:
        last_turns[number] = turn
        turn += 1

    del last_turns[numbers[-1]]

    last_spoken = numbers[-1]
    while turn <= limit:
        next_spoken = 0
        if last_spoken in last_turns:
            next_spoken = turn - last_turns[last_spoken] - 1
        last_turns[last_spoken] = turn - 1
        turn += 1
        last_spoken = next_spoken

    print(last_spoken)


if __name__ == '__main__':
    main()
