import re
import math

INPUT_FILE = "input.txt"


def remove_field(fields, index):
    to_remove = list(fields[index])[0]

    for i in fields:
        if i != index:
            fields[i].discard(to_remove)

    return fields


def part_two(ticket_info):
    my_ticket = ticket_info.split("\n\n")[1].split("\n")[1].split(",")

    valid_tickets = get_valid_tickets(ticket_info)
    validation = ticket_info.split("\n\n")[0]
    lines = validation.split("\n")
    validation_data = {val.split(':')[0]: get_allowed(val.split(':')[1]) for val in lines}

    possible_fields = {}
    for i in range(len(valid_tickets[0])):
        possible_fields[i] = set()
        for name, numb_range in validation_data.items():
            x = [y for y in valid_tickets if int(y[i]) not in numb_range]
            if len(x) == 0:
                possible_fields[i].add(name)

    print("test")
    while True:
        for index, fields in possible_fields.items():
            if len(fields) == 1:
                possible_fields = remove_field(possible_fields, index)

        a = [x for x in possible_fields if len(possible_fields[x]) > 1]
        if len(a) == 0:
            break
    possible_fields = [list(possible_fields[i])[0] for i in possible_fields]
    field_index = {possible_fields[i]: i for i in range(len(possible_fields))}
    departure_index = [field_index[i] for i in field_index if i.startswith('departure')]
    departure_values = [int(my_ticket[i]) for i in departure_index]

    print(math.prod(departure_values))


def get_valid_tickets(ticket_info):
    validation = ticket_info.split("\n\n")[0]
    allowed = get_allowed(validation)
    tickets = ticket_info.split("\n\n")[2].split("\n")[1:]
    tickets = list(map(lambda x: x.split(','), tickets))
    return [ticket for ticket in tickets if all(int(element) in allowed for element in ticket)]


def get_allowed(validation):
    ranges = re.findall(r"\d+-\d+", validation)
    allowed = set()
    for allowed_range in ranges:
        allowed_range = allowed_range.split('-')
        allowed.update(range(int(allowed_range[0]), int(allowed_range[1]) + 1))
    return allowed


def main():
    ticket_info = load_input()

    part_one(ticket_info)
    part_two(ticket_info)


def part_one(ticket_info):
    validation = ticket_info.split("\n\n")[0]
    ranges = re.findall(r"\d+-\d+", validation)
    allowed = set()
    for allowed_range in ranges:
        allowed_range = allowed_range.split('-')
        allowed.update(range(int(allowed_range[0]), int(allowed_range[1]) + 1))
    numbers = re.findall(r"\d+", ticket_info.split("\n\n")[2])
    print(sum([int(number) for number in numbers if int(number) not in allowed]))


def load_input():
    return open(INPUT_FILE, "r").read().strip()


if __name__ == '__main__':
    main()
