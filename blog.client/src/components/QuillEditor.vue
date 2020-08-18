
<template>
  <div>
    <v-tabs v-model="tab" background-color="#eee" :centered="true">
      <v-tabs-slider></v-tabs-slider>

      <v-tab>რედაქტირება</v-tab>

      <v-tab-item>
        <v-card flat tile>
          <v-card-text>
            <v-row>
              <v-col cols="12" sm="6">
                <!-- <v-file-input label="File input" v-model="file" prepend-icon="mdi-camera"></v-file-input> -->
                <v-file-input
                  v-model="file"
                  color="deep-purple accent-4"
                  accept="image/png, image/jpeg, image/bmp"
                  counter
                  multiple
                  placeholder="აირჩიეთ ფაილი"
                  prepend-icon="mdi-camera"
                  :show-size="1000"
                >
                  <template v-slot:selection="{ index, text }">
                    <v-chip
                      v-if="index < 2"
                      color="deep-purple accent-4"
                      dark
                      label
                      small
                    >{{ text }}</v-chip>

                    <span
                      v-else-if="index === 2"
                      class="overline grey--text text--darken-3 mx-2"
                    >+{{ fileUploadModal.files.length - 2 }} File(s)</span>
                  </template>
                </v-file-input>
                <v-select :items="categoryOptions" v-model="category" label="კატეგორია"></v-select>
                <v-text-field label="სათაური" v-model="title"></v-text-field>
              </v-col>
              <v-col cols="12" sm="6">
                <v-textarea outlined rows="7" v-model="shortDescription" label="მოკლე აღწერა"></v-textarea>
              </v-col>
              <v-col cols="12" sm="6"></v-col>
              <v-col cols="12" sm="6"></v-col>
            </v-row>
            <div class="example">
              <v-file-input
                style="display:none"
                type="file"
                id="fileUploadId"
                v-model="myFile"
                @change="uploadFileManualy()"
              />

              <quill-editor
                class="editor"
                ref="myTextEditor"
                :value="content"
                :options="editorOption"
                @change="onEditorChange"
                @blur="onEditorBlur($event)"
                @focus="onEditorFocus($event)"
                @ready="onEditorReady($event)"
              />
              <!-- <button @click="cl">click</button> -->
            </div>
          </v-card-text>
          <br />
          <br />
          <br />
          <br />
          <v-card-actions>
            <v-btn class="ma-2" outlined color="indigo" @click="save">შენახვა</v-btn>
            <v-btn v-if="id" class="ma-2" outlined color="red" @click="openDeleteArticleDialog">წაშლა</v-btn>
          </v-card-actions>
        </v-card>
      </v-tab-item>
      <v-tab>ნახვა</v-tab>

      <v-tab-item>
        <v-card flat tile>
          <v-card-text>
            <div class="output ql-snow">
              <div class="ql-editor" v-html="content"></div>
            </div>
          </v-card-text>
        </v-card>
      </v-tab-item>
    </v-tabs>
    <br />

    <v-dialog v-model="deleteDialog" max-width="290">
      <v-card>
        <v-card-title class="headline red lighten-2">წაშლა</v-card-title>
        <br />
        <v-card-text>
          <h3>გსურთ სტატიის წაშლა?</h3>
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn color="green darken-1" text @click="deleteDialog = false">არა</v-btn>

          <v-btn color="red darken-1" text @click="deleteArticle">დიახ</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import constants from "../constants";

import dedent from "dedent";
import hljs from "highlight.js";
import debounce from "lodash/debounce";
import { quillEditor } from "vue-quill-editor";

// highlight.js style
import "highlight.js/styles/tomorrow.css";

// import theme style
import "quill/dist/quill.core.css";
import "quill/dist/quill.snow.css";

export default {
  name: "QuillEditor",
  title: "Theme: snow",
  components: {
    quillEditor
  },
  props: ["id"],
  data() {
    return {
      prefixURL: constants.API_PREFIX,
      deleteDialog: false,
      tab: 0,
      text: "aaaaaa",
      myFile: null,
      editorOption: {
        modules: {
          toolbar: [
            ["bold", "italic", "underline", "strike"],
            ["blockquote", "code-block"],
            [{ header: 1 }, { header: 2 }],
            [{ list: "ordered" }, { list: "bullet" }],
            [{ script: "sub" }, { script: "super" }],
            [{ indent: "-1" }, { indent: "+1" }],
            [{ direction: "rtl" }],
            [{ size: ["small", false, "large", "huge"] }],
            [{ header: [1, 2, 3, 4, 5, 6, false] }],
            [{ font: [] }],
            [{ color: [] }, { background: [] }],
            [{ align: [] }],
            ["clean"],
            ["link", "image", "video"]
          ]
        }
      },
      title: "",
      category: 0,
      shortDescription: "",
      imageName: "",
      imagePath: "",
      file: [],
      content: dedent`
        `
    };
  },
  methods: {
    async deleteArticle() {
      console.log("delete");
      var result = await this.$http.deleteArticle(this.id);

      this.$toast(result.message, {
        color: result.isSuccess ? "green" : "red",
        dismissable: true
      });

      if (result.isSuccess) {
        this.$router.push({ path: "/Main" });
        return;
      }

    },
    async openDeleteArticleDialog() {
      console.log("delete");
      this.deleteDialog = true;

    },
    async save() {
      if (this.id) {
        var dataEdit = {
          id: Number(this.id),
          imageName: this.imageName,
          imagePath: this.imagePath,
          title: this.title,
          category: this.category,
          shortDescription: this.shortDescription,
          content: this.content
        };

        var resultEdit = await this.$http.updateArticle(dataEdit);

        this.$toast(resultEdit.message, {
          color: resultEdit.isSuccess ? "green" : "red",
          dismissable: true
        });

        console.log("edit");
        return;
      }
      let formData = new FormData();
      console.log(this.file);
      for (var i = 0; i != this.file.length; i++) {
        formData.append("files", this.file[i]);
        if (this.file.length > 5) {
          return;
        }
        if (this.file.size > 10547290) {
          return;
        }
      }

      var resultFiles = await this.$http.postFiles(formData);
      console.log(resultFiles);

      if (!resultFiles.isSuccess) {
        this.$toast(resultFiles.message, {
          color: "red",
          dismissable: true
        });
        return;
      }

      if (!this.title || !this.category || !this.shortDescription) {
        this.$toast("შეავსეთ ველები", {
          color: "red",
          dismissable: true
        });
        return;
      }
      var data = {
        imageName: this.file[0].name,
        imagePath: resultFiles.message,
        title: this.title,
        category: this.category,
        shortDescription: this.shortDescription,
        content: this.content
      };

      var result = await this.$http.saveArticle(data);

      this.$toast(result.message, {
        color: result.isSuccess ? "green" : "red",
        dismissable: true
      });

      // if (result.isSuccess) {
      //   (this.title = ""),
      //     (this.category = ""),
      //     (this.shortDescription = ""),
      //     (this.content = "");
      //     this.file = []
      // }

      console.log(result);
    },
    uploadFileManualy() {
      console.log("vvvvvv");
    },
    showImageUI(item) {
      var input = document.getElementById("fileUploadId");
      input.click();
      var that = this;
      input.onchange = async e => {
        var file = e.target.files[0];
        let formData = new FormData();
        console.log(this.file);
        // for (var i = 0; i != this.file.length; i++) {
        //   formData.append("files", this.file[i]);
        //   if (this.file.length > 5) {
        //     return;
        //   }
        //   if (this.file.size > 10547290) {
        //     return;
        //   }
        // }
        formData.append("files", file);
        var resultFiles = await this.$http.postFiles(formData);
        console.log(resultFiles);

        if (!resultFiles.isSuccess) {
          this.$toast(resultFiles.message, {
            color: "red",
            dismissable: true
          });
          return;
        }

        this.content +=
          " <img width='500' height='600' src=" +
          this.prefixURL +
          resultFiles.message +
          " />";

        console.log("e.target file", file, that.content);
      };
      console.log("itenm", item);
    },
    onEditorChange: debounce(function(value) {
      this.content = value.html;
    }, 466),
    onEditorBlur(editor) {
      console.log("editor blur!", editor);
    },
    onEditorFocus(editor) {
      console.log("editor focus!", editor);
    },
    onEditorReady(editor) {
      console.log("editor ready!", editor);
    }
  },
  computed: {
    categoryOptions() {
      return [
        { text: "- აირჩიეთ -", value: null },
        { text: "კატეგორია1", value: 1 },
        { text: "კატეგორია2", value: 2 },
        { text: "კატეგორია3", value: 3 },
        { text: "კატეგორია4", value: 4 },
        { text: "კატეგორია5", value: 5 }
      ];
    },
    editor() {
      return this.$refs.myTextEditor.quill;
    },
    contentCode() {
      return hljs.highlightAuto(this.content).value;
    }
  },
  async created() {
    console.log("id", this.id);
    if (this.id) {
      var result = await this.$http.getArticle(this.id);
      this.content = result.content;
      this.title = result.title;
      this.category = result.category;
      this.shortDescription = result.shortDescription;
      this.imageName = result.imageName;
      this.imagePath = result.imagePath;
      this.content = result.content;

      console.log(result);
    }
  },
  mounted() {
    console.log("this is Quill instance:", this.editor);
    var toolbar = this.$refs.myTextEditor.quill.getModule("toolbar");
    toolbar.addHandler("image", this.showImageUI);
  }
};
</script>

<style lang="scss" scoped>
.ql-video {
  width: 100%;
}
.example {
  display: flex;
  flex-direction: column;

  .editor {
    height: 40rem;
  }

  .output {
    width: 100%;
    height: 20rem;
    margin: 0;
    border: 1px solid #ccc;
    overflow-y: auto;
    resize: vertical;

    &.code {
      padding: 1rem;
      height: 16rem;
    }

    &.ql-snow {
      border-top: none;
      height: 24rem;
    }
  }
}
</style>