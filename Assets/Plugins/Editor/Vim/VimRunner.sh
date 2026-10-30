#!/bin/bash
#[Configuration]
TERM="gnome-terminal"

if [ ! -z `vim --serverlist | grep Unity3d` ]; then
    vim --servername Unity3d --remote-silent +$2 $1
else
    $TERM -- "vim --servername Unity3d --remote-silent +$2 $1"
fi
