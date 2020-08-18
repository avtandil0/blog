<template>
  <div>
    <div class="editorx_body">
      <!--editorjs id-->
      <div class id="codex-editor" />
    </div>
    <!-- <button style="margin-left: 30%;" type="button" name="button" @click="save()">save</button> -->
    <div class="editorx_body">
      <pre>{{value}}</pre>
    </div>
  </div>
</template>

<script>
import EditorJS from "@editorjs/editorjs";
import Header from "@editorjs/header";
import Paragraph from "@editorjs/paragraph";
import List from "@editorjs/list";
// import SimpleImage  from "@editorjs/simple-image";
import Delimiter from "@editorjs/delimiter";
import ImageTool from "@editorjs/image";
import LinkTool from "@editorjs/link";

export default {
  name: "Editor",
  data() {
    return {
      value: null
    };
  },
  async created() {
    await this.myEditor();

    this.onReady();
  },
  methods: {
    divClick() {
      console.log("divClick");
    },
    onReady() {
      console.log(" onReadyonReadyonReadyonReady");
      let editable_elements = document.querySelectorAll(
        "[contenteditable=true]"
      );
      editable_elements.forEach(el => el.removeAttribute("contenteditable"));

      let icon_settings = document.querySelectorAll(
        ".ce-toolbar__settings-btn"
      );
      icon_settings.forEach(el => el.remove());

      let icon_settings1 = document.querySelectorAll(".ce-toolbar__plus");
      icon_settings1.forEach(el => el.remove());
    },
    save: function() {
      // eslint-disable-next-line no-undef
      editor.save().then(savedData => {
        // var cc = document.querySelectorAll('[contenteditable=true]')
        // console.log("1111111111111", cc);
        // document.getElementById("codex-editor").contentEditable = false;
        this.onReady();
        console.log("aaaaaaaaaaa", JSON.stringify(savedData));
        this.value = savedData;
      });
    },
    myEditor: function() {
      var that = this;
      window.editor = new EditorJS({
        data: JSON.parse('{"time":1593848557548,"blocks":[{"type":"header","data":{"text":"1914 translation by H. Rackham","level":2}},{"type":"paragraph","data":{"text":"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum"}},{"type":"delimiter","data":{}},{"type":"list","data":{"style":"ordered","items":["choose1","choose2","choose3"]}},{"type":"paragraph","data":{"text":"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum"}},{"type":"paragraph","data":{"text":"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum"}},{"type":"delimiter","data":{}},{"type":"header","data":{"text":"The standard Lorem Ipsum passage, used since the 1500s","level":2}},{"type":"paragraph","data":{"text":"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum"}},{"type":"paragraph","data":{"text":"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum"}},{"type":"paragraph","data":{"text":"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum"}}],"version":"2.18.0"}'),
        holder: "codex-editor",
        autofocus: true,
        /**
         * This Tool will be used as default
         */
        initialBlock: "paragraph",
        tools: {
          linkTool: {
            class: LinkTool,

          },
          image: {
            class: ImageTool
          },
          delimiter: Delimiter,
          // image: SimpleImage,
          header: {
            class: Header,
            data: { text: "Hello Vue/Editor.js", level: 2 }
          },
          list: {
            class: List
          },
          paragraph: {
            class: Paragraph,
            data: {
              text: "aaaaaaaaaaaas        assssssssssss"
            }
          }
        },
        onReady: function() {
          console.log("ready");
          that.onReady()
        },
        onChange: function() {
          console.log("change");
           that.onReady()
        }
      });
    }
  },
  mounted: function() {}
};
</script>

<style lang="css" scoped >
</style>