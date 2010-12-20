﻿using System;

namespace Bounce.Framework {
    class BounceCommandBuild : IBounceCommand {
        public BounceCommandBuild(IBounceCommand cleanAfterBuildCommand, string commandLineCommand) {
            CleanAfterBuildCommand = cleanAfterBuildCommand;
            CommandLineCommand = commandLineCommand;
        }

        public void InvokeCommand(Action onBuild, Action onClean) {
            onBuild();
        }

        public string InfinitiveTense {
            get { return "build"; }
        }

        public string PastTense {
            get { return "built"; }
        }

        public string PresentTense {
            get { return "building"; }
        }

        public IBounceCommand CleanAfterBuildCommand { get; private set; }

        public string CommandLineCommand { get; private set; }
    }

    internal interface IBounceCommandParser {
        IBounceCommand Parse(string command);
        IBounceCommand Build { get; }
        IBounceCommand Clean { get; }
    }

    class BounceCommandParser : IBounceCommandParser {
        private IBounceCommand _build;
        private IBounceCommand _buildAndKeep;
        private IBounceCommand _clean;

        public IBounceCommand Parse(string command) {
            switch (command) {
                case "build":
                    return Build;
                case "buildandkeep":
                    return BuildAndKeep;
                case "clean":
                    return Clean;
                default:
                    return null;
            }
        }

        public IBounceCommand Build {
            get {
                if (_build == null) {
                    _build = new BounceCommandBuild(Clean, "build");
                }
                return _build;
            }
        }

        public IBounceCommand BuildAndKeep {
            get {
                if (_buildAndKeep == null) {
                    _buildAndKeep = new BounceCommandBuild(null, "buildandkeep");
                }
                return _buildAndKeep;
            }
        }

        public IBounceCommand Clean {
            get {
                if (_clean == null) {
                    _clean = new BounceCommandClean();
                }
                return _clean;
            }
        }
    }

    internal class BounceCommandClean : IBounceCommand {
        public void InvokeCommand(Action onBuild, Action onClean) {
            onClean();
        }

        public string InfinitiveTense {
            get { return "clean"; }
        }

        public string PastTense {
            get { return "cleaned"; }
        }

        public string PresentTense {
            get { return "cleaning"; }
        }

        public IBounceCommand CleanAfterBuildCommand {
            get { return null; }
        }

        public string CommandLineCommand {
            get { return "clean"; }
        }
    }
}