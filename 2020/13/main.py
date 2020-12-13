import functools

INPUT_FILE = "input.txt"


def main():
    #   part_one()
    part_two()


def part_one():
    arrival, lines = load_input()
    wait_time = [{'id': line_id, 'wait_time': line_id - (arrival % line_id)} for line_id in lines]
    min_wait_time = min(wait_time, key=lambda x: x['wait_time'])
    print(min_wait_time['id'] * min_wait_time['wait_time'])


def part_two():
    input_lines = open(INPUT_FILE, "r").read().strip().split("\n")

    lines = input_lines[1].split(",")

    a = []
    m = []
    pos = []

    for i in range(len(lines)):
        if lines[i].isnumeric():
            line_id = int(lines[i])
            wait_time = - i % line_id
            a.append(wait_time)
            m.append(line_id)
            pos.append(i)

    print(int(float(chinese_remainder(m, a))))


def chinese_remainder(m_s, a_s):
    prod = functools.reduce(lambda u, b: u * b, m_s)
    x = 0
    for i in range(len(m_s)):
        m = m_s[i]
        m_i = prod // m
        _, r, s = extended_euclid(m, m_i)
        x = x + a_s[i] * s * m_i

    for i in range(len(m_s)):
        m = m_s[i]
        a = a_s[i]
        print(x % m)
        print(a % m)

    return x % prod


def extended_euclid(a, b):
    if b == 0:
        return a, 1, 0
    d, s, t = extended_euclid(b, a % b)
    d, s, t = (d, t, s - (a // b) * t)
    return d, s, t


def load_input():
    input_lines = open(INPUT_FILE, "r").read().strip().split("\n")
    arrival = int(input_lines[0])
    lines = [int(line_id) for line_id in input_lines[1].split(",") if line_id.isnumeric()]
    return arrival, lines


if __name__ == '__main__':
    main()
