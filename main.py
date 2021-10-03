import os
from AntClass import Ant

ant_list = []


def command_runner(user_input):
    if user_input == "add":
        add_ant()
    elif user_input == "remove":
        remove_ant()
    elif user_input == "list":
        list_ant()
    elif user_input == "find":
        find_ant(input("Enter name: "))
    elif user_input == "help":
        help_user()
    elif user_input == "clear":
        os.system("clear")
    elif user_input == "exit":
        quit()
    elif user_input == "sort":
        print("Not implemented")
        list_ant()
    else:
        pass


def list_ant():
    ID = 0
    for ant in ant_list:
        print(f"ID: {ID} : {ant.get_name()} with {ant.get_legs()} legs")
        ID += 1


def sort_ant():
    # NOT GOING TO THIS PYTHON SUCKS
    pass


def find_ant(feedback):
    name = feedback.capitalize().strip()
    for ant in ant_list:
        if ant.get_name() == name:
            print(f"{ant.get_name()} with {ant.get_legs()} legs")


def remove_ant():
    list_ant()
    ant_list.pop(int(input("Enter ID to remove ant: ")))
    print()
    list_ant()


def help_user():
    print(f"Help - shows all commands and description \n"
          f"Add - adds a ant to the stack \n"
          f"Remove - removes a ant from the stack \n"
          f"List - lists all ant in the stack \n"
          f"Sort - sorts ants by properties \n"
          f"Find - finds a specific ant by name \n"
          f"Clear - clear the terminal \n"
          f"Exit - quits the program \n")


def has_numbers(stri):
    return any(char.isdigit() for char in stri)


def check_dupe(ant_name):
    for ant in ant_list:
        if ant.get_name() == ant_name:
            return True


def add_ant():
    ant_name = ""
    ant_legs = 0

    while True:
        ant_name = input("Enter a name: \n")
        if 3 < len(ant_name) < 12:
            if has_numbers(ant_name):
                print("name does not allow numbers")
            else:
                if check_dupe(ant_name.capitalize()):
                    print("Can't have dupe ant")
                else:
                    break
        else:
            print("To long/short of a name")

    while True:
        try:
            ant_legs = int(input("Enter a amount of legs: \n"))
        except():
            print("Can't have letters only numbers \n")
            continue # check if it goes back at top of loop

        if 0 < ant_legs < 7:
            break
        else:
            print("To many legs")

    ant_name = ant_name.capitalize().strip()
    ant_list.append(Ant(ant_name, ant_legs))


if __name__ == '__main__':
    while True:
        command_runner(input("Enter a command: "))


