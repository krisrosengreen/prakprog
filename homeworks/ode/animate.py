import numpy as np
from matplotlib.animation import FuncAnimation
import matplotlib.pyplot as plt

data = np.loadtxt("Out.txt", delimiter=",")
fig, ax = plt.subplots()

max_frames = 100

line1, = ax.plot([], [])
line2, = ax.plot([], [])
line3, = ax.plot([], [])

ax.set_xlim(-100, 100)
ax.set_ylim(-100, 100)

t0 = np.min(data[:,0])
tf = np.max(data[:,0])

tstep = (tf - t0)/max_frames
indeces = []

for i in range(max_frames):
    argmin = np.argmin(np.abs(data[:, 0] - tstep*i))
    indeces.append(argmin)

def update(frame):
    line1.set_data(data[:, 1][:frame], data[:, 2][:frame])
    line2.set_data(data[:, 3][:frame], data[:, 4][:frame])
    line3.set_data(data[:, 5][:frame], data[:, 6][:frame])
    return line1, line2, line3

ani = FuncAnimation(fig, update, frames=indeces, interval=1000)

plt.show()
