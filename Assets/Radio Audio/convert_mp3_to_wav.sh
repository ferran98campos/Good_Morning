#!/bin/bash

# specify directory where mp3 files are located
DIRECTORY="exposition/"

# loop over all the mp3 files in the directory
for file in "$DIRECTORY"/*_raw.mp3
do
  # get the base name of the file (without extension)
  filename=$(basename -- "$file")
  filename="${filename%.*}"
  
  # remove '_raw' from filename
  filename="${filename%_raw}"
  
  # convert mp3 to wav using ffmpeg
  ffmpeg -i "$file" "$DIRECTORY/$filename.wav"
done
