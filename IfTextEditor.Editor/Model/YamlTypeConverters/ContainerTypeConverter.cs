using System;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace IfTextEditor.Editor.Model.YamlTypeConverters
{
    public class ContainerTypeConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type)
        {
            return typeof(FileContainer).IsAssignableFrom(type);
        }

        #region Read

        public object ReadYaml(IParser parser, Type type)
        {
            var container = new FileContainer();

            parser.Expect<MappingStart>();

            //File name
            var fileName = parser.Expect<Scalar>();
            if (fileName.Value != string.Empty)
                container.Name = fileName.Value;

            //Messages
            parser.Expect<SequenceStart>();
            while (parser.Allow<SequenceEnd>() == null)
            {
                ReadMessage(parser, container);
            }

            parser.Expect<MappingEnd>();
            return container;
        }

        private static void ReadMessage(IParser parser, FileContainer cont)
        {
            var msg = new FileContainer.Message();

            //Name
            parser.Expect<MappingStart>();
            var msgName = parser.Expect<Scalar>();
            if (msgName.Value != string.Empty)
                msg.MsgName = msgName.Value;

            //Pages
            parser.Expect<SequenceStart>();
            while (parser.Allow<SequenceEnd>() == null)
            {
                ReadPage(parser, msg);
            }

            parser.Expect<MappingEnd>();
            cont.Messages.Add(msg);
        }

        private static void ReadPage(IParser parser, FileContainer.Message msg)
        {
            var page = new FileContainer.Message.Page();

            //Commands
            parser.Expect<MappingStart>();
            var cmdsName = parser.Expect<Scalar>();
            if (cmdsName.Value != nameof(page.Commands))
                throw new YamlException($"\"{nameof(page.Commands)}\" scalar expected.");

            parser.Expect<SequenceStart>();
            while (parser.Allow<SequenceEnd>() == null)
            {
                ReadCommand(parser, page);
            }
            parser.Expect<MappingEnd>();

            //Comments
            parser.Expect<MappingStart>();
            var commentsName = parser.Expect<Scalar>();
            if(commentsName.Value != nameof(page.Comments))
                throw new YamlException($"\"{nameof(page.Comments)}\" scalar expected.");

            parser.Expect<MappingStart>();
            while (parser.Allow<MappingEnd>() == null)
            {
                var commentIndex = parser.Expect<Scalar>();
                var commentContent = parser.Expect<Scalar>();

                page.Comments.Add(Convert.ToInt32(commentIndex.Value), commentContent.Value);
            }
            parser.Expect<MappingEnd>();

            //Dialogue
            parser.Expect<MappingStart>();
            var dialogeName = parser.Expect<Scalar>();
            if(dialogeName.Value != nameof(page.SpokenLine))
                throw new YamlException($"\"{nameof(page.SpokenLine)}\" scalar expected.");

            string[] dialogue = parser.Expect<Scalar>().Value.Split(new[] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in dialogue)
                page.SpokenLine.Add(str.Trim());

            parser.Expect<MappingEnd>();

            //Page end
            parser.Expect<MappingStart>();
            var pageEnd = parser.Expect<Scalar>();
            if (pageEnd.Value != CommandType.PageEnd.ToString())
                throw new YamlException($"\"{CommandType.PageEnd}\" scalar expected.");

            var symbol = parser.Expect<Scalar>();

            if (symbol.Value != string.Empty)
            {
                var pgEndCmd = new Command(symbol.Value, new string[0], CommandType.PageEnd);
                page.Commands.Add(pgEndCmd);
            }

            parser.Expect<MappingEnd>();
            msg.Pages.Add(page);
        }

        private static void ReadCommand(IParser parser, FileContainer.Message.Page page)
        {
            parser.Expect<MappingStart>();

            var cmdType = parser.Expect<Scalar>();
            var type = (CommandType) Enum.Parse(typeof(CommandType), cmdType.Value);
            string symbol = string.Empty;
            var parameters = new List<string>();

            switch (type)
            {
                case CommandType.UNKNOWN_TYPE:
                case CommandType.Other:
                    parser.Expect<SequenceStart>();
                    symbol = parser.Expect<Scalar>().Value;
                    while (parser.Allow<SequenceEnd>() == null)
                        parameters.Add(parser.Expect<Scalar>().Value);
                    break;
                case CommandType.Format:
                case CommandType.PageEnd:
                    symbol = parser.Expect<Scalar>().Value;
                    break;
                default:
                    parser.Expect<SequenceStart>();
                    while (parser.Allow<SequenceEnd>() == null)
                        parameters.Add(parser.Expect<Scalar>().Value);
                    break;
            }

            var cmd = new Command(symbol, parameters.ToArray(), type);
            page.Commands.Add(cmd);
            parser.Expect<MappingEnd>();
        }
        #endregion

        #region Write

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            var container = (FileContainer)value;

            //File name
            emitter.Emit(new MappingStart());
            emitter.Emit(new Scalar(container.Name));

            //Messages
            emitter.Emit(new SequenceStart(null, null, true, SequenceStyle.Block));
            foreach (FileContainer.Message msg in container)
                EmitMessage(emitter, msg);

            emitter.Emit(new SequenceEnd());
            emitter.Emit(new MappingEnd());
        }

        private static void EmitMessage(IEmitter emitter, FileContainer.Message msg)
        {
            emitter.Emit(new MappingStart());
            emitter.Emit(new Scalar(msg.MsgName));

            emitter.Emit(new SequenceStart(null, null, true, SequenceStyle.Block));
            foreach (FileContainer.Message.Page page in msg)
            {
                EmitPage(emitter, page);
                emitter.Emit(new Comment(string.Empty, false));
            }

            emitter.Emit(new SequenceEnd());
            emitter.Emit(new MappingEnd());
        }

        private static void EmitPage(IEmitter emitter, FileContainer.Message.Page page)
        {
            //Commands
            emitter.Emit(new MappingStart());
            emitter.Emit(new Scalar(nameof(page.Commands)));

            emitter.Emit(new SequenceStart(null, null, true, SequenceStyle.Block));
            foreach (Command cmd in page)
                if (cmd.Type != CommandType.PageEnd)
                    EmitCommand(emitter, cmd);

            emitter.Emit(new SequenceEnd());
            emitter.Emit(new MappingEnd());

            //Comments
            emitter.Emit(new MappingStart());
            emitter.Emit(new Scalar(nameof(page.Comments)));

            emitter.Emit(new MappingStart());
            foreach (KeyValuePair<int, string> entry in page.Comments)
            {                
                emitter.Emit(new Scalar(entry.Key.ToString()));
                emitter.Emit(new Scalar(null, null, entry.Value, ScalarStyle.DoubleQuoted, false, true));
            }

            emitter.Emit(new MappingEnd());
            emitter.Emit(new MappingEnd());

            //Dialogue
            emitter.Emit(new MappingStart());
            emitter.Emit(new Scalar(nameof(page.SpokenLine)));

            string line = string.Join(Environment.NewLine, page.SpokenLine);
            emitter.Emit(new Scalar(null, null, line, ScalarStyle.Literal, true, false));

            emitter.Emit(new MappingEnd());

            //Page end
            emitter.Emit(new MappingStart());
            emitter.Emit(new Scalar(CommandType.PageEnd.ToString()));

            foreach (Command cmd in page)
            {
                if (cmd.Type == CommandType.PageEnd)
                {
                    emitter.Emit(new Scalar(cmd.Symbol));
                    emitter.Emit(new MappingEnd());
                    return;
                }
            }

            emitter.Emit(new Scalar(string.Empty));
            emitter.Emit(new MappingEnd());

            foreach(Command cmd in page)
                if (cmd.Type == CommandType.PageEnd)
                    EmitCommand(emitter, cmd);
        }

        private static void EmitCommand(IEmitter emitter, Command cmd)
        {
            emitter.Emit(new MappingStart());

            switch (cmd.Type)
            {
                case CommandType.UNKNOWN_TYPE:
                case CommandType.Other:
                    emitter.Emit(new Scalar(cmd.Type.ToString()));
                    emitter.Emit(new SequenceStart(null, null, true, SequenceStyle.Flow));
                    emitter.Emit(new Scalar(cmd.Symbol));
                    foreach (string param in cmd)
                        emitter.Emit(new Scalar(param));
                    emitter.Emit(new SequenceEnd());
                    break;
                case CommandType.Format:
                case CommandType.PageEnd:
                    emitter.Emit(new Scalar(cmd.Type.ToString()));
                    emitter.Emit(new Scalar(cmd.Symbol));
                    break;
                default:
                    emitter.Emit(new Scalar(cmd.Type.ToString()));
                    emitter.Emit(new SequenceStart(null, null, true, SequenceStyle.Flow));
                    foreach (string param in cmd)
                        emitter.Emit(new Scalar(param));
                    emitter.Emit(new SequenceEnd());
                    break;
            }

            emitter.Emit(new MappingEnd());
        }
        #endregion
    }
}
