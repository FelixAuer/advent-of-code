import itertools

INPUT_FILE = "input.txt"


def part_one(lines):
    mask = ''
    memory = {}

    for line in lines:
        if line.startswith('mask'):
            mask = line.split()[2]
        if line.startswith('mem'):
            address = line.split()[0]
            address = int(address.replace('mem[', '').replace(']', ''))
            value = format(int(line.split()[2]), '036b')
            memory[address] = add_mask(value, mask)

    values = [int(val, 2) for val in memory.values()]
    print(sum(values))


def add_mask(value, mask):
    retval = ''
    for i in range(len(value)):
        if mask[i] != 'X':
            retval += mask[i]
        else:
            retval += value[i]

    return retval


def get_floating_addresses(address, mask):
    retval = ''
    for i in range(len(address)):
        if mask[i] != '0':
            retval += mask[i]
        else:
            retval += address[i]

    address = retval

    floating_addresses = []
    count_x = mask.count('X')
    permutations = list(itertools.product("01", repeat=count_x))
    for permutation in permutations:
        replaced = address
        for i in range(len(permutation)):
            replaced = replaced.replace('X', permutation[i], 1)
        floating_addresses.append(replaced)
    return floating_addresses


def part_two(lines):
    mask = ''
    memory = {}

    for line in lines:
        if line.startswith('mask'):
            mask = line.split()[2]
        if line.startswith('mem'):
            address = line.split()[0]
            address = format(int(address.replace('mem[', '').replace(']', '')), '036b')
            value = int(line.split()[2])
            addresses = get_floating_addresses(address, mask)
            for address in addresses:
                memory[int(address, 2)] = value

    print(sum(memory.values()))


def main():
    lines = load_input()

    part_one(lines)
    part_two(lines)


def load_input():
    return open(INPUT_FILE, "r").read().strip().split("\n")


if __name__ == '__main__':
    main()
