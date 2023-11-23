import tkinter as tk

from tkinter.colorchooser import askcolor

root = tk.Tk()
root.title("Color")
canvas = tk.Canvas(root, bg='white')

def start_drawing(event):
    global is_drawing, prev_x, prev_y
    is_drawing = True
    prev_x, prev_y = event.x, event.y

def draw(event):
    global is_drawing, prev_x, prev_y
    if is_drawing:
        current_x, current_y = event.x, event.y
        # canvas.create_line(prev_x, prev_y, current_x, current_y, fill=dra