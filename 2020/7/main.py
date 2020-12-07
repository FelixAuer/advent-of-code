import re

INPUT_FILE = "input.txt"


def main():
    rules = load_input()
    part_one(rules)
    part_two(rules)


def part_two(rules):
    print(count_bags(rules, 'shiny gold') - 1)


def count_bags(rules, bag):
    rule = [rule for rule in rules if rule.startswith(bag)][0]

    inner_bags = re.findall(r"(\d [a-z]* [a-z]*) bags?[,.]", rule)

    sum_inner = 0

    for inner_bag in inner_bags:
        multiplier = inner_bag.split()[0]
        sum_inner = sum_inner + int(multiplier) * count_bags(rules, ' '.join(inner_bag.split()[-2:]))

    return sum_inner + 1


def part_one(rules):
    bags = ['shiny gold']
    outer_count = 0
    while len(bags) > 0:
        bag = bags.pop(0)

        regex = r"contain.*" + bag

        containing_bags = [' '.join(rule.split()[:2]) for rule in rules if re.search(regex, rule)]
        bags.extend(containing_bags)
        rules = [rule for rule in rules if not re.search(regex, rule)]

        outer_count = outer_count + len(containing_bags)
    print(outer_count)


def load_input():
    return open(INPUT_FILE, "r").read().strip().split("\n")


if __name__ == '__main__':
    main()
