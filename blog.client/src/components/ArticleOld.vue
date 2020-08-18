<template >
  <div>
    <Editor/>
    <!-- <editor
      autofocus
      ref="editor"
      holder-id="codex-editor"
      save-button-id="save-button"
      :init-data="initData"
      @save="save"
      @ready="onReady"
      @change="onChange"
    /> -->
     <!-- <v-btn color="primary" text @click="save">Save</v-btn> -->
    <!-- <span v-html="article"></span> -->
  </div>
</template>

<script>
// import { Editor } from "vue-editor-js";
import Editor from "../components/Editor.vue";

export default {
  name: "ArticleOld",
  components: {
    // Editor,
    Editor
  },
  props: ["id", "article", "dd"],
  data: () => ({
    initData: {
      time: 1554508385558,
      blocks: [
        { type: "header", data: { text: "Hello Vue/Editor.js", level: 2 } },
        {
          type: "list",
          data: {
            style: "ordered",
            items: ["Learn Vue.js<br>", "Learn Editor.js"]
          }
        },
        {
          type: "quote",
          data: {
            text: "This is awesome block based editor.<br>",
            caption: "",
            alignment: "left"
          }
        },
        { type: "delimiter", data: {} },
        {
          type: "warning",
          data: {
            title: "Warning",
            message: "Open Development Tools console"
          }
        },
        { type: "delimiter", data: {} }
      ],
      version: "2.12.3"
    }
  }),
  created() {
    console.log("this.$refs.editor",this.$refs.editor);
  },
  methods: {
    save() {
      console.log(this.$refs.editor);
      // this.$refs.editor.save();
    },
    onSave(response) {
      console.log(JSON.stringify(response));
    },
    onReady() {
      console.log("ready");
    },
    onChange() {
      console.log("changed");
    }
  }
};
</script>
